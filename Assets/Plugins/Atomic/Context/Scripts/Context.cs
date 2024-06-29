using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Contexts
{
    public sealed class Context : IContext
    {
        #region Base

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        
        private string name;

        public Context(string name = null)
        {
            this.name = name;
        }

        #endregion

        #region Data

        public event Action<int, object> OnDataAdded;
        public event Action<int, object> OnDataDeleted;
        public event Action<int, object> OnDataChanged;

        public IReadOnlyDictionary<int, object> AllData => this.allData;

        private readonly Dictionary<int, object> allData = new();

        public bool HasData(int key)
        {
            return this.allData.ContainsKey(key);
        }

        public T GetData<T>(int key) where T : class
        {
            if (this.allData.TryGetValue(key, out object value))
            {
                return value as T;
            }

            return null;
        }

        public object GetData(int key)
        {
            if (this.allData.TryGetValue(key, out var value))
            {
                return value;
            }

            return null;
        }

        public bool AddData(int key, object value)
        {
            if (this.allData.TryAdd(key, value))
            {
                this.OnDataAdded?.Invoke(key, value);
                return true;
            }

            return false;
        }

        public bool TryGetData<T>(int id, out T value) where T : class
        {
            if (this.allData.TryGetValue(id, out object field))
            {
                value = field as T;
                return true;
            }

            value = default;
            return false;
        }

        public bool TryGetData(int id, out object value)
        {
            return this.allData.TryGetValue(id, out value);
        }

        public void SetData(int key, object value)
        {
            this.allData[key] = value;
            this.OnDataChanged?.Invoke(key, value);
        }

        public bool DelData(int key)
        {
            if (this.allData.Remove(key, out object removed))
            {
                this.OnDataDeleted?.Invoke(key, removed);
                return true;
            }

            return false;
        }

        public bool DelData(int key, out object removed)
        {
            if (this.allData.Remove(key, out removed))
            {
                this.OnDataDeleted?.Invoke(key, removed);
                return true;
            }

            return false;
        }

        #endregion

        #region Systems

        public event Action<ISystem> OnSystemAdded;
        public event Action<ISystem> OnSystemRemoved;

        public IReadOnlyCollection<ISystem> AllSystems => this.allSystems;

        private readonly HashSet<ISystem> allSystems = new();

        public T GetSystem<T>() where T : ISystem
        {
            foreach (ISystem system in this.allSystems)
            {
                if (system is T tSystem)
                {
                    return tSystem;
                }
            }

            return default;
        }

        public bool TryGetSystem<T>(out T result) where T : ISystem
        {
            foreach (ISystem system in this.allSystems)
            {
                if (system is T tSystem)
                {
                    result = tSystem;
                    return true;
                }
            }

            result = default;
            return false;
        }
        
        public bool HasSystem(ISystem system)
        {
            return this.allSystems.Contains(system);
        }

        public bool HasSystem<T>() where T : ISystem
        {
            foreach (ISystem system in this.allSystems)
            {
                if (system is T)
                {
                    return true;
                }
            }

            return false;
        }

        public bool AddSystem<T>() where T : ISystem, new()
        {
            return this.AddSystem(new T());
        }

        public bool AddSystem(ISystem system)
        {
            if (!this.allSystems.Add(system))
            {
                return false;
            }

            if (this.state is > ContextState.OFF and < ContextState.DESTROYED && 
                system is IInitSystem initSystem)
            {
                initSystem.Init(this);
            }

            if (this.state == ContextState.ENABLED && system is IEnableSystem enableSystem)
            {
                enableSystem.Enable(this);
            }

            if (system is IUpdateSystem update)
            {
                this.updateSystems.Add(update);
            }

            if (system is IFixedUpdateSystem fixedUpdate)
            {
                this.fixedUpdateSystems.Add(fixedUpdate);
            }

            if (system is ILateUpdateSystem lateUpdate)
            {
                this.lateUpdateSystems.Add(lateUpdate);
            }

            this.OnSystemAdded?.Invoke(system);
            return true;
        }

        public bool DelSystem<T>() where T : ISystem
        {
            T system = this.GetSystem<T>();
            if (system == null)
            {
                return false;
            }

            return this.DelSystem(system);
        }

        public bool DelSystem(ISystem system)
        {
            if (!this.allSystems.Remove(system))
            {
                return false;
            }

            if (system is IUpdateSystem update)
            {
                this.updateSystems.Remove(update);
            }

            if (system is IFixedUpdateSystem fixedUpdate)
            {
                this.fixedUpdateSystems.Remove(fixedUpdate);
            }

            if (system is ILateUpdateSystem lateUpdate)
            {
                this.lateUpdateSystems.Remove(lateUpdate);
            }
            
            if (this.state == ContextState.ENABLED && system is IDisableSystem disableSystem)
            {
                disableSystem.Disable(this);
            }

            if (this.state is > ContextState.OFF and < ContextState.DESTROYED && 
                system is IDestroySystem destroySystem)
            {
                destroySystem.Destroy(this);
            }

            this.OnSystemRemoved?.Invoke(system);
            return true;
        }
        
        #endregion

        #region Parent

        public IContext Parent { get; set; }

        public IReadOnlyList<IContext> Children { get; }

        #endregion
        
        #region Lifecycle

        public ContextState State
        {
            get { return this.state; }
        }

        private ContextState state = ContextState.OFF;

        public event Action<ContextState> OnStateChanged;

        private readonly List<IUpdateSystem> updateSystems = new();
        private readonly List<IFixedUpdateSystem> fixedUpdateSystems = new();
        private readonly List<ILateUpdateSystem> lateUpdateSystems = new();

        private readonly List<IUpdateSystem> _updateCache = new();
        private readonly List<IFixedUpdateSystem> _fixedUpdateCache = new();
        private readonly List<ILateUpdateSystem> _lateUpdateCache = new();

        public void Initialize()
        {
            if (this.state != ContextState.OFF)
            {
                Debug.LogWarning(
                    $"You can initialize context only from {ContextState.OFF} state! (Actual state: {this.state})");
                return;
            }

            foreach (ISystem system in this.allSystems)
            {
                if (system is IInitSystem initSystem)
                {
                    initSystem.Init(this);
                }
            }

            this.state = ContextState.INITIALIZED;
            this.OnStateChanged?.Invoke(this.state);
        }

        public void Enable()
        {
            if (this.state != ContextState.INITIALIZED)
            {
                Debug.LogWarning(
                    $"You can enable context only from {ContextState.INITIALIZED} state! (Actual state: {this.state})");
                return;
            }

            foreach (ISystem system in this.allSystems)
            {
                if (system is IEnableSystem enableSystem)
                {
                    enableSystem.Enable(this);
                }
            }

            this.state = ContextState.ENABLED;
            this.OnStateChanged?.Invoke(this.state);
        }

        public void Update(float deltaTime)
        {
            if (this.state != ContextState.ENABLED)
            {
                return;
            }

            if (this.updateSystems.Count == 0)
            {
                return;
            }

            _updateCache.Clear();
            _updateCache.AddRange(this.updateSystems);

            for (int i = 0, count = _updateCache.Count; i < count; i++)
            {
                IUpdateSystem update = _updateCache[i];
                update.Update(this, deltaTime);
            }
        }

        public void FixedUpdate(float deltaTime)
        {
            if (this.state != ContextState.ENABLED)
            {
                return;
            }

            if (this.fixedUpdateSystems.Count == 0)
            {
                return;
            }

            _fixedUpdateCache.Clear();
            _fixedUpdateCache.AddRange(this.fixedUpdateSystems);

            for (int i = 0, count = _fixedUpdateCache.Count; i < count; i++)
            {
                IFixedUpdateSystem updateSystem = _fixedUpdateCache[i];
                updateSystem.FixedUpdate(this, deltaTime);
            }
        }

        public void LateUpdate(float deltaTime)
        {
            if (this.state != ContextState.ENABLED)
            {
                return;
            }

            if (this.lateUpdateSystems.Count == 0)
            {
                return;
            }

            _lateUpdateCache.Clear();
            _lateUpdateCache.AddRange(this.lateUpdateSystems);

            for (int i = 0, count = _lateUpdateCache.Count; i < count; i++)
            {
                ILateUpdateSystem updateSystem = _lateUpdateCache[i];
                updateSystem.LateUpdate(this, deltaTime);
            }
        }

        public void Disable()
        {
            if (this.state != ContextState.ENABLED)
            {
                Debug.LogWarning(
                    $"You can disable context only from {ContextState.ENABLED} state! (Actual state: {this.state})");
                return;
            }

            foreach (ISystem system in this.allSystems)
            {
                if (system is IDisableSystem disableSystem)
                {
                    disableSystem.Disable(this);
                }
            }

            this.state = ContextState.DISABLED;
            this.OnStateChanged?.Invoke(this.state);
        }

        public void Destroy()
        {
            if (this.state != ContextState.DISABLED)
            {
                Debug.LogWarning(
                    $"You can destroy context only from {ContextState.DISABLED} state! (Actual state: {this.state})");
                return;
            }

            foreach (ISystem system in this.allSystems)
            {
                if (system is IDestroySystem destroySystem)
                {
                    destroySystem.Destroy(this);
                }
            }

            this.state = ContextState.DESTROYED;
            this.OnStateChanged?.Invoke(this.state);
        }

        #endregion
    }
}