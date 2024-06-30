using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Contexts
{
    public sealed class Context : IContext
    {
        #region Main

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public IContext Parent
        {
            get => this.parent;
            set => this.parent = value;
        }

        private string name;
        private IContext parent;

        public Context(string name = null, IContext parent = null)
        {
            this.name = name;
            this.parent = parent;
        }

        public bool IsParent(IContext context)
        {
            return this.parent == context;
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

        public bool AddValue(int key, object value)
        {
            if (this.values.TryAdd(key, value))
            {
                this.OnValueAdded?.Invoke(key, value);
                return true;
            }

            return false;
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

            if (this.initialized && system is IInitSystem initSystem)
            {
                initSystem.Init(this);
            }

            if (this.enabled && system is IEnableSystem enableSystem)
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

            if (this.enabled && system is IDisableSystem disableSystem)
            {
                disableSystem.Disable(this);
            }

            if (this.initialized && system is IDisposeSystem disposeSystem)
            {
                disposeSystem.Dispose(this);
            }

            this.OnSystemRemoved?.Invoke(system);
            return true;
        }

        #endregion

        #region Parent

        #endregion

        #region Lifecycle

        public event Action OnInitiazized;
        public event Action OnEnabled;
        public event Action OnDisabled;
        public event Action OnDisposed;

        public event Action<float> OnUpdate;
        public event Action<float> OnFixedUpdate;
        public event Action<float> OnLateUpdate;

        public bool Initialized => this.initialized;
        public bool Enabled => this.enabled;
        
        private bool initialized;
        private bool enabled;

        private readonly List<IUpdateSystem> updates = new();
        private readonly List<IFixedUpdateSystem> fixedUpdates = new();
        private readonly List<ILateUpdateSystem> lateUpdates = new();

        private readonly List<IUpdateSystem> _updateCache = new();
        private readonly List<IFixedUpdateSystem> _fixedUpdateCache = new();
        private readonly List<ILateUpdateSystem> _lateUpdateCache = new();

        public void Initialize()
        {
            if (this.initialized)
            {
                Debug.LogWarning($"Context with name {name} is already initialized!");
                return;
            }

            foreach (ISystem system in this.systems)
            {
                if (system is IInitSystem initSystem)
                {
                    initSystem.Init(this);
                }
            }

            this.initialized = true;
            this.OnInitiazized?.Invoke();
        }

        public void Enable()
        {
            if (this.enabled)
            {
                Debug.LogWarning($"Context with name {name} is already enabled!");
                return;
            }
            
            if (!this.initialized)
            {
                Debug.LogError($"You can enable context only after initialize! Context: {name}");
                return;
            }

            foreach (ISystem system in this.systems)
            {
                if (system is IEnableSystem enableSystem)
                {
                    enableSystem.Enable(this);
                }
            }

            this.enabled = true;
            this.OnEnabled?.Invoke();
        }

        public void Update(float deltaTime)
        {
            if (!this.enabled)
            {
                Debug.LogError($"You can update context if it's enabled! Context {name}");
                return;
            }

            int count = this.updates.Count;
            if (count != 0)
            {
                _updateCache.Clear();
                _updateCache.AddRange(this.updates);

                for (int i = 0; i < count; i++)
                {
                    IUpdateSystem update = _updateCache[i];
                    update.Update(this, deltaTime);
                }
            }

            this.OnUpdate?.Invoke(deltaTime);
        }

        public void FixedUpdate(float deltaTime)
        {
            if (!this.enabled)
            {
                Debug.LogError($"You can update context if it's enabled! Context {name}");
                return;
            }

            int count = this.fixedUpdates.Count;
            if (count != 0)
            {
                _fixedUpdateCache.Clear();
                _fixedUpdateCache.AddRange(this.fixedUpdates);

                for (int i = 0; i < count; i++)
                {
                    IFixedUpdateSystem updateSystem = _fixedUpdateCache[i];
                    updateSystem.FixedUpdate(this, deltaTime);
                }
            }

            this.OnFixedUpdate?.Invoke(deltaTime);
        }

        public void LateUpdate(float deltaTime)
        {
            if (!this.enabled)
            {
                Debug.LogError($"You can update context if it's enabled! Context {name}");
                return;
            }

            int count = this.lateUpdates.Count;
            if (count != 0)
            {
                _lateUpdateCache.Clear();
                _lateUpdateCache.AddRange(this.lateUpdates);

                for (int i = 0; i < count; i++)
                {
                    ILateUpdateSystem updateSystem = _lateUpdateCache[i];
                    updateSystem.LateUpdate(this, deltaTime);
                }
            }

            this.OnLateUpdate?.Invoke(deltaTime);
        }

        public void Disable()
        {
            if (!this.enabled)
            {
                Debug.LogWarning($"Context with {name} is not enabled!");
                return;
            }

            foreach (ISystem system in this.systems)
            {
                if (system is IDisableSystem disableSystem)
                {
                    disableSystem.Disable(this);
                }
            }

            this.enabled = false;
            this.OnDisabled?.Invoke();
        }

        public void Dispose()
        {
            if (!this.initialized)
            {
                Debug.LogWarning($"Context with name {name} is not initialized!");
            }
            
            if (this.enabled)
            {
                this.Disable();
            }

            foreach (ISystem system in this.systems)
            {
                if (system is IDisposeSystem destroySystem)
                {
                    destroySystem.Dispose(this);
                }
            }

            this.initialized = false;
            this.OnDisposed?.Invoke();
        }

        #endregion
    }
}