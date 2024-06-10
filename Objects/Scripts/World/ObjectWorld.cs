using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Atomic.Objects
{
    public sealed class ObjectWorld : IObjectWorld
    {
        #region Objects

        public event Action<IObject> OnObjectSpawned;
        public event Action<IObject> OnObjectUnspawned;

        private readonly Dictionary<int, IObject> objectMap = new();
        private readonly List<IObject> objectList = new();
        private readonly Queue<int> recycledIds = new();

        public IReadOnlyList<IObject> Objects
        {
            get { return this.objectList; }
        }
        
        public IObject GetObject(int objectId)
        {
            return this.objectMap[objectId];
        }

        public IObject SpawnObject(string name = null, bool enabled = true, bool autoRun = true)
        {
            int id = this.GetFreeId();

            Object obj = new Object(id, name, enabled);
            obj.OnTagAdded += this.OnTagAdded;
            obj.OnTagDeleted += this.OnTagRemoved;
            obj.OnValueAdded += this.OnValueAdded;
            obj.OnValueDeleted += this.OnValueDeleted;
            obj.OnValueChanged += this.OnValueChanged;
            obj.OnEnabled += this.OnEnableObject;
            obj.OnDisabled += this.OnDisableObject;

            this.objectMap.Add(id, obj);
            this.objectList.Add(obj);
            this.OnObjectSpawned?.Invoke(obj);

            if (autoRun)
            {
                if (this.constructed)
                {
                    this.ConstructObject(obj);
                }

                if (this.enabled)
                {
                    this.OnEnableObject(obj);
                }
            }

            return obj;
        }

        public bool UnspawnObject(IObject obj)
        {
            return this.UnspawnObject(obj.Id);
        }

        public bool UnspawnObject(int objectId)
        {

            if (!this.objectMap.Remove(objectId, out IObject obj))
            {
                return false;
            }

            this.objectList.Remove(obj);
            this.OnDisableObject(obj);
            this.DisposeObject(obj);


            obj.OnTagAdded -= this.OnTagAdded;
            obj.OnTagDeleted -= this.OnTagRemoved;
            obj.OnValueAdded -= this.OnValueAdded;
            obj.OnValueDeleted -= this.OnValueDeleted;
            obj.OnValueChanged -= this.OnValueChanged;
            obj.OnEnabled -= this.OnEnableObject;
            obj.OnDisabled -= this.OnDisableObject;

            this.ClearTagPools(obj);
            this.ClearValuePools(obj);

            this.recycledIds.Enqueue(objectId);
            this.OnObjectUnspawned?.Invoke(obj);
            obj.Dispose();
            return true;
        }
        
        public void ConstructObject(IObject obj)
        {
            if (!this.constructed)
            {
                return;
            }
            
            if (obj.Constructed)
            {
                return;
            }

            obj.OnLogicAdded += this.OnLogicAdded;
            obj.OnLogicDeleted += this.OnLogicDeleted;
            obj.Constructed = true;

            IReadOnlyList<ILogic> logics = obj.Logics;

            int count = logics.Count;
            if (count == 0)
            {
                return;
            }

            for (int i = 0; i < count; i++)
            {
                if (logics[i] is IConstructable composable)
                {
                    composable.Construct(obj);
                }
            }

        }
        
        public void OnDisableObject(IObject obj)
        {
            if (!this.enabled)
            {
                return;
            }
            
            if (!obj.Constructed)
            {
                return;
            }

            // if (!obj.Enabled)
            // {
            //     return;
            // }

            IReadOnlyList<ILogic> logics = obj.Logics;

            int count = logics.Count;
            if (count == 0)
            {
                return;
            }

            for (int i = 0; i < count; i++)
            {
                ILogic logic = logics[i];
                if (logic is IDisable disable)
                {
                    disable.Disable();
                }

                if (logic is IUpdate update)
                {
                    this.tickables.Remove(update);
                }

                if (logic is IFixedUpdate fixedUpdate)
                {
                    this.fixedTickables.Remove(fixedUpdate);
                }

                if (logic is ILateUpdate lateUpdate)
                {
                    this.lateTickables.Remove(lateUpdate);
                }
            }
        }
        
        public void OnEnableObject(IObject obj)
        {
            if (!this.enabled)
            {
                return;
            }
            
            if (!obj.Constructed)
            {
                this.ConstructObject(obj);
            }

            if (!obj.Enabled)
            {
                return;
            }

            IReadOnlyList<ILogic> logics = obj.Logics;

            int count = logics.Count;
            if (count == 0)
            {
                return;
            }

            for (int i = 0; i < count; i++)
            {
                ILogic logic = logics[i];
                if (logic is IEnable enable)
                {
                    enable.Enable();
                }

                if (logic is IUpdate update)
                {
                    this.tickables.Add(update);
                }

                if (logic is IFixedUpdate fixedUpdate)
                {
                    this.fixedTickables.Add(fixedUpdate);
                }

                if (logic is ILateUpdate lateUpdate)
                {
                    this.lateTickables.Add(lateUpdate);
                }
            }
        }

        public void DisposeObject(IObject obj)
        {
            if (!obj.Constructed)
            {
                return;
            }
            
            obj.OnLogicAdded -= this.OnLogicAdded;
            obj.OnLogicDeleted -= this.OnLogicDeleted;
            
            IReadOnlyList<ILogic> logics = obj.Logics;

            int count = logics.Count;
            if (count == 0)
            {
                return;
            }

            for (int i = 0; i < count; i++)
            {
                if (logics[i] is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }

            obj.Constructed = false;
        }

        private int GetFreeId()
        {
            return this.recycledIds.TryDequeue(out int id) ? id : this.objectList.Count;
        }

        #endregion

        #region Lifecycle

        public bool Constructed
        {
            get { return this.constructed; }
        }

        public bool Enabled
        {
            get { return this.enabled; }
        }

        private bool constructed;
        private bool enabled;

        public void Construct()
        {
            if (this.constructed)
            {
                return;
            }
            
            this.constructed = true;

            int count = this.objectList.Count;
            if (count == 0)
            {
                return;
            }

            for (int i = 0; i < count; i++)
            {
                IObject obj = this.objectList[i];
                this.ConstructObject(obj);
            }
        }

        public void Enable()
        {
            if (this.enabled)
            {
                return;
            }
            
            this.enabled = true;

            int count = this.objectList.Count;
            if (count == 0)
            {
                return;
            }

            for (int i = 0; i < count; i++)
            {
                IObject obj = this.objectList[i];
                this.OnEnableObject(obj);
            }
        }
        

        public void Disable()
        {
            if (!this.enabled)
            {
                return;
            }
            
            this.enabled = false;

            int count = this.objectList.Count;
            if (count == 0)
            {
                return;
            }

            for (int i = 0; i < count; i++)
            {
                IObject obj = this.objectList[i];
                this.OnDisableObject(obj);
            }

        }

        public void Dispose()
        {
            if (!this.constructed)
            {
                return;
            }
            
            this.constructed = false;

            if (this.enabled)
            {
                this.Disable();
            }

            int[] objectIds = this.objectMap.Keys.ToArray();
            for (int i = 0, count = objectIds.Length; i < count; i++)
            {
                int objectId = objectIds[i];
                this.UnspawnObject(objectId);
            }

        }

        #endregion

        #region Updates

        private readonly List<IUpdate> tickables = new();
        private readonly List<IFixedUpdate> fixedTickables = new();
        private readonly List<ILateUpdate> lateTickables = new();

        private readonly List<IUpdate> _updateCache = new();
        private readonly List<IFixedUpdate> _fixedUpdateCache = new();
        private readonly List<ILateUpdate> _lateUpdateCache = new();

        public void Tick(float deltaTime)
        {
            if (!this.enabled)
            {
                return;
            }

            if (this.tickables.Count == 0)
            {
                return;
            }

            _updateCache.Clear();
            _updateCache.AddRange(this.tickables);

            for (int i = 0, count = _updateCache.Count; i < count; i++)
            {
                IUpdate update = _updateCache[i];
                update.OnUpdate(deltaTime);
            }
        }

        public void FixedTick(float deltaTime)
        {
            if (!this.enabled)
            {
                return;
            }

            if (this.fixedTickables.Count == 0)
            {
                return;
            }

            _fixedUpdateCache.Clear();
            _fixedUpdateCache.AddRange(this.fixedTickables);

            for (int i = 0, count = _fixedUpdateCache.Count; i < count; i++)
            {
                IFixedUpdate atomicFixedUpdate = _fixedUpdateCache[i];
                atomicFixedUpdate.OnFixedUpdate(deltaTime);
            }
        }

        public void LateTick(float deltaTime)
        {
            if (!this.enabled)
            {
                return;
            }

            if (this.lateTickables.Count == 0)
            {
                return;
            }

            _lateUpdateCache.Clear();
            _lateUpdateCache.AddRange(this.lateTickables);

            for (int i = 0, count = _lateUpdateCache.Count; i < count; i++)
            {
                ILateUpdate lateUpdate = _lateUpdateCache[i];
                lateUpdate.OnLateUpdate(deltaTime);
            }
        }

        private void OnLogicAdded(IObject obj, ILogic logic)
        {
            if (!obj.Constructed)
            {
                return;
            }

            if (logic is IConstructable composable)
            {
                composable.Construct(obj);
            }

            if (!obj.Enabled)
            {
                return;
            }

            if (logic is IEnable enable)
            {
                enable.Enable();
            }

            if (logic is IUpdate update)
            {
                this.tickables.Add(update);
            }

            if (logic is IFixedUpdate fixedUpdate)
            {
                this.fixedTickables.Add(fixedUpdate);
            }

            if (logic is ILateUpdate lateUpdate)
            {
                this.lateTickables.Add(lateUpdate);
            }
        }

        private void OnLogicDeleted(IObject obj, ILogic logic)
        {
            if (obj.Enabled)
            {
                if (logic is IUpdate tickable)
                {
                    this.tickables.Remove(tickable);
                }

                if (logic is IFixedUpdate fixedTickable)
                {
                    this.fixedTickables.Remove(fixedTickable);
                }

                if (logic is ILateUpdate lateTickable)
                {
                    this.lateTickables.Remove(lateTickable);
                }

                if (logic is IDisable disable)
                {
                    disable.Disable();
                }
            }

            if (obj.Constructed)
            {
                if (logic is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }

        #endregion

        #region Pools

        private readonly Dictionary<int, HashSet<IObject>> tagPools = new();

        public IObject GetObjectWithTag(int tag)
        {
            if (!this.tagPools.TryGetValue(tag, out var pool))
            {
                return null;
            }

            if (pool.Count == 0)
            {
                return null;
            }

            return pool.ElementAt(0);
        }

        public IObject[] GetObjectsWithTag(int tag)
        {
            if (!this.tagPools.TryGetValue(tag, out var pool))
            {
                pool = new HashSet<IObject>();
                this.tagPools.Add(tag, pool);
            }

            return pool.ToArray();
        }

        private void OnTagAdded(IObject obj, int tag)
        {
            if (!this.tagPools.TryGetValue(tag, out var pool))
            {
                pool = new HashSet<IObject>();
                this.tagPools.Add(tag, pool);
            }

            pool.Add(obj);
        }

        private void OnTagRemoved(IObject obj, int tag)
        {
            if (this.tagPools.TryGetValue(tag, out var pool))
            {
                pool.Remove(obj);
            }
        }

        private void ClearTagPools(IObject obj)
        {
            foreach (int tag in obj.Tags)
            {
                this.OnTagRemoved(obj, tag);
            }
        }

        private void ClearValuePools(IObject obj)
        {
            //TODO:
        }

        private void OnValueChanged(IObject arg1, int arg2, object arg3)
        {
            //TODO:
        }

        private void OnValueDeleted(IObject arg1, int arg2, object arg3)
        {
            //TODO:
        }

        private void OnValueAdded(IObject arg1, int arg2, object arg3)
        {
            //TODO:
        }

        #endregion
    }
}