using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Contexts
{
    public sealed class Context : IContext
    {
        #region Main

        public event Action<ContextState> OnStateChanged;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public ContextState State
        {
            get { return this.state; }
        }

        private string name;
        private ContextState state = ContextState.OFF;

        public Context(string name = null)
        {
            this.name = name;
        }

        public void Initialize()
        {
            if (this.state != ContextState.OFF)
            {
                Debug.LogWarning($"You can initialize context only from {ContextState.OFF} state! (Actual state: {this.state})");
                return;
            }

            for (int i = 0, count = this.allSystems.Count; i < count; i++)
            {
                ISystem system = this.allSystems[i];
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
                Debug.LogWarning($"You can enable context only from {ContextState.INITIALIZED} state! (Actual state: {this.state})");
                return;
            }
            
            for (int i = 0, count = this.allSystems.Count; i < count; i++)
            {
                ISystem system = this.allSystems[i];
                if (system is IEnableSystem enableSystem)
                {
                    enableSystem.Enable(this);
                }
            }

            this.state = ContextState.ENABLED;
            this.OnStateChanged?.Invoke(this.state);
        }

        #endregion

        #region Data

        public event Action<int, object> OnDataAdded;
        public event Action<int, object> OnDataDeleted;
        public event Action<int, object> OnDataChanged;

        public IReadOnlyDictionary<int, object> AllData { get; }

        #endregion

        #region System

        public event Action<ISystem> OnSystemAdded;
        public event Action<ISystem> OnSystemRemoved;

        public IReadOnlyList<ISystem> AllSystems
        {
            get { return this.allSystems; }
        }

        private List<ISystem> allSystems = new();

        #endregion

        #region Parent

        public IContext Parent { get; set; }

        public IReadOnlyList<IContext> Children { get; }

        #endregion

        public bool AddData(int key, object value)
        {
            throw new System.NotImplementedException();
        }

        public void SetData(int key, object value)
        {
            throw new NotImplementedException();
        }

        public bool DelData(int key)
        {
            throw new NotImplementedException();
        }

        public bool DelData(int key, out object removed)
        {
            throw new NotImplementedException();
        }

        public bool HasData(int key)
        {
            throw new NotImplementedException();
        }

        public T GetData<T>(int key) where T : class
        {
            throw new System.NotImplementedException();
        }

        public object GetData(int key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetData<T>(int id, out T value) where T : class
        {
            throw new NotImplementedException();
        }

        public bool TryGetData(int id, out object value)
        {
            throw new NotImplementedException();
        }

        public T GetSystem<T>() where T : ISystem
        {
            throw new NotImplementedException();
        }

        public bool TryGetLogic<T>(out T logic) where T : ISystem
        {
            throw new NotImplementedException();
        }

        public bool AddSystem(ISystem system)
        {
            if (this.allSystems.Contains(system))
            {
                return false;
            }
            
            this.allSystems.Add(system);
            return true;
            // throw new System.NotImplementedException();
        }

        //
        // private void OnLogicAdded(IObject obj, ILogic logic)
        // {
        //     if (!obj.Constructed)
        //     {
        //         return;
        //     }
        //
        //     if (logic is IConstructable composable)
        //     {
        //         composable.Init(obj);
        //     }
        //
        //     if (!obj.Enabled)
        //     {
        //         return;
        //     }
        //
        //     if (logic is IEnable enable)
        //     {
        //         enable.Enable();
        //     }
        //
        //     if (logic is IUpdate update)
        //     {
        //         this.updateSystems.Add(update);
        //     }
        //
        //     if (logic is IFixedUpdate fixedUpdate)
        //     {
        //         this.fixedUpdateSystems.Add(fixedUpdate);
        //     }
        //
        //     if (logic is ILateUpdate lateUpdate)
        //     {
        //         this.lateUpdateSystems.Add(lateUpdate);
        //     }
        // }
        //
        // private void OnLogicDeleted(IObject obj, ILogic logic)
        // {
        //     if (obj.Enabled)
        //     {
        //         if (logic is IUpdate tickable)
        //         {
        //             this.updateSystems.Remove(tickable);
        //         }
        //
        //         if (logic is IFixedUpdate fixedTickable)
        //         {
        //             this.fixedUpdateSystems.Remove(fixedTickable);
        //         }
        //
        //         if (logic is ILateUpdate lateTickable)
        //         {
        //             this.lateUpdateSystems.Remove(lateTickable);
        //         }
        //
        //         if (logic is IDisable disable)
        //         {
        //             disable.Disable();
        //         }
        //     }
        //
        //     if (obj.Constructed)
        //     {
        //         if (logic is IDisposable disposable)
        //         {
        //             disposable.Dispose();
        //         }
        //     }
        // }

        public bool AddSystem<T>() where T : ISystem
        {
            throw new System.NotImplementedException();
        }

        public bool DelSystem(ISystem system)
        {
            throw new NotImplementedException();
        }

        public bool DelSystem<T>() where T : ISystem
        {
            throw new NotImplementedException();
        }

        public bool HasSystem(ISystem system)
        {
            throw new NotImplementedException();
        }

        public bool HasSystem<T>() where T : ISystem
        {
            throw new NotImplementedException();
        }

        public bool AddSystem(int key, ISystem logic)
        {
            throw new System.NotImplementedException();
        }

        public void AddComponent(int key, ISystem obj)
        {
            throw new System.NotImplementedException();
        }


        #region Updates

        private readonly List<IUpdateSystem> updateSystems = new();
        private readonly List<IFixedUpdateSystem> fixedUpdateSystems = new();
        private readonly List<ILateUpdateSystem> lateUpdateSystems = new();

        private readonly List<IUpdateSystem> _updateCache = new();
        private readonly List<IFixedUpdateSystem> _fixedUpdateCache = new();
        private readonly List<ILateUpdateSystem> _lateUpdateCache = new();

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

        #endregion

        #region Construct

        public void Construct()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}