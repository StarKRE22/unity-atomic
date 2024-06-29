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

        public Context(string name = null, IContext parent = null)
        {
            this.name = name;
            this.SetParent(parent);
        }

        #endregion

        #region Values

        public event Action<int, object> OnValueAdded;
        public event Action<int, object> OnValueDeleted;
        public event Action<int, object> OnValueChanged;

        public IReadOnlyDictionary<int, object> Values => this.values;

        private readonly Dictionary<int, object> values = new();

        public bool HasValue(int key)
        {
            return this.values.ContainsKey(key);
        }

        public T GetValue<T>(int key) where T : class
        {
            if (this.values.TryGetValue(key, out object value))
            {
                return value as T;
            }

            return null;
        }

        public object GetValue(int key)
        {
            if (this.values.TryGetValue(key, out var value))
            {
                return value;
            }

            return null;
        }


        public bool AddValue(int key, object value)
        {
            if (this.values.TryAdd(key, value))
            {
                this.OnValueAdded?.Invoke(key, value);
                return true;
            }

            return false;
        }

        public bool TryGetValue<T>(int id, out T value) where T : class
        {
            if (this.values.TryGetValue(id, out object field))
            {
                value = field as T;
                return true;
            }

            value = default;
            return false;
        }

        public bool TryGetValue(int id, out object value)
        {
            return this.values.TryGetValue(id, out value);
        }

        public void SetValue(int key, object value)
        {
            this.values[key] = value;
            this.OnValueChanged?.Invoke(key, value);
        }

        public bool DelValue(int key)
        {
            if (this.values.Remove(key, out object removed))
            {
                this.OnValueDeleted?.Invoke(key, removed);
                return true;
            }

            return false;
        }

        public bool DelValue(int key, out object removed)
        {
            if (this.values.Remove(key, out removed))
            {
                this.OnValueDeleted?.Invoke(key, removed);
                return true;
            }

            return false;
        }

        #endregion

        #region Systems

        public event Action<ISystem> OnSystemAdded;
        public event Action<ISystem> OnSystemRemoved;

        public IReadOnlyCollection<ISystem> Systems => this.systems;

        private readonly HashSet<ISystem> systems = new();

        public T GetSystem<T>() where T : ISystem
        {
            foreach (ISystem system in this.systems)
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
            foreach (ISystem system in this.systems)
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
            return this.systems.Contains(system);
        }

        public bool HasSystem<T>() where T : ISystem
        {
            foreach (ISystem system in this.systems)
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
            if (!this.systems.Add(system))
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
                this.updates.Add(update);
            }

            if (system is IFixedUpdateSystem fixedUpdate)
            {
                this.fixedUpdates.Add(fixedUpdate);
            }

            if (system is ILateUpdateSystem lateUpdate)
            {
                this.lateUpdates.Add(lateUpdate);
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
            if (!this.systems.Remove(system))
            {
                return false;
            }

            if (system is IUpdateSystem update)
            {
                this.updates.Remove(update);
            }

            if (system is IFixedUpdateSystem fixedUpdate)
            {
                this.fixedUpdates.Remove(fixedUpdate);
            }

            if (system is ILateUpdateSystem lateUpdate)
            {
                this.lateUpdates.Remove(lateUpdate);
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
        
        public IContext Parent => this.parent;
        public IReadOnlyCollection<IContext> Children => this.children;
        public ICollection<IContext> ChildrenUnsafe => this.children;

        private readonly HashSet<IContext> children = new();
        private IContext parent;

        public bool IsChild(IContext context)
        {
            return this.children.Contains(context);
        }

        public bool IsParent(IContext context)
        {
            return this.parent == context;
        }

        public bool AddChild(IContext child)
        {
            return child != null && child.SetParent(this);
        }

        public bool DelChild(IContext child)
        {
            return child != null && child.SetParent(null);
        }

        public bool SetParent(IContext parent)
        {
            if (this.parent == parent)
            {
                return false;
            }

            this.parent?.ChildrenUnsafe.Remove(this);
            this.parent = parent;
            this.parent?.ChildrenUnsafe.Add(this);
            return true;
        }

        #endregion

        #region Lifecycle

        public ContextState State
        {
            get { return this.state; }
        }

        private ContextState state = ContextState.OFF;

        public event Action<ContextState> OnStateChanged;

        private readonly List<IUpdateSystem> updates = new();
        private readonly List<IFixedUpdateSystem> fixedUpdates = new();
        private readonly List<ILateUpdateSystem> lateUpdates = new();

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

            foreach (ISystem system in this.systems)
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

            foreach (ISystem system in this.systems)
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

            if (this.updates.Count == 0)
            {
                return;
            }

            _updateCache.Clear();
            _updateCache.AddRange(this.updates);

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

            if (this.fixedUpdates.Count == 0)
            {
                return;
            }

            _fixedUpdateCache.Clear();
            _fixedUpdateCache.AddRange(this.fixedUpdates);

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

            if (this.lateUpdates.Count == 0)
            {
                return;
            }

            _lateUpdateCache.Clear();
            _lateUpdateCache.AddRange(this.lateUpdates);

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

            foreach (ISystem system in this.systems)
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

            foreach (ISystem system in this.systems)
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