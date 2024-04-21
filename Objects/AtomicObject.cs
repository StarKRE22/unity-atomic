using System.Collections.Generic;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Objects
{
    public class AtomicObject : AtomicEntity, IMutableAtomicObject,
        IAtomicEnable,
        IAtomicDisable,
        IAtomicUpdate,
        IAtomicFixedUpdate,
        IAtomicLateUpdate
    {
        public bool IsEnable => _enabled;

#if ODIN_INSPECTOR
        [Title("Logic"), PropertySpace, PropertyOrder(150)]
        [ShowInInspector, HideInEditorMode]
#endif
        private readonly List<IAtomicLogic> allLogics = new();

        private readonly List<IAtomicEnable> enables = new();
        private readonly List<IAtomicDisable> disables = new();
        private readonly List<IAtomicUpdate> updates = new();
        private readonly List<IAtomicFixedUpdate> fixedUpdates = new();
        private readonly List<IAtomicLateUpdate> lateUpdates = new();

        private readonly List<IAtomicEnable> _enableCache = new();
        private readonly List<IAtomicDisable> _disableCache = new();
        private readonly List<IAtomicUpdate> _updateCache = new();
        private readonly List<IAtomicFixedUpdate> _fixedUpdateCache = new();
        private readonly List<IAtomicLateUpdate> _lateUpdateCache = new();

        private bool _enabled;
        
        public void AddLogic(IAtomicLogic target)
        {
            if (target == null)
            {
                return;
            }

            this.allLogics.Add(target);

            if (target is IAtomicEnable enable)
            {
                this.enables.Add(enable);

                if (_enabled)
                {
                    enable.Enable();
                }
            }

            if (target is IAtomicDisable disable)
            {
                this.disables.Add(disable);
            }

            if (target is IAtomicUpdate update)
            {
                this.updates.Add(update);
            }

            if (target is IAtomicFixedUpdate fixedUpdate)
            {
                this.fixedUpdates.Add(fixedUpdate);
            }

            if (target is IAtomicLateUpdate lateUpdate)
            {
                this.lateUpdates.Add(lateUpdate);
            }
        }

        public void RemoveLogic(IAtomicLogic target)
        {
            if (target == null)
            {
                return;
            }

            if (!this.allLogics.Remove(target))
            {
                return;
            }

            if (target is IAtomicEnable enable)
            {
                this.enables.Remove(enable);
            }

            if (target is IAtomicUpdate tickable)
            {
                this.updates.Remove(tickable);
            }

            if (target is IAtomicFixedUpdate fixedTickable)
            {
                this.fixedUpdates.Remove(fixedTickable);
            }

            if (target is IAtomicLateUpdate lateTickable)
            {
                this.lateUpdates.Remove(lateTickable);
            }

            if (target is IAtomicDisable disable)
            {
                if (_enabled)
                {
                    disable.Disable();
                }
            }
        }

        public IAtomicLogic[] AllLogic()
        {
            return this.allLogics.ToArray();
        }

        public int AllLogicNonAlloc(IAtomicLogic[] results)
        {
            int i = 0;

            foreach (var element in this.allLogics)
            {
                results[i++] = element;
            }

            return i;
        }

        public IList<IAtomicLogic> AllLogicUnsafe()
        {
            return this.allLogics;
        }

        public void Enable()
        {
            _enabled = true;

            if (this.enables.Count == 0)
            {
                return;
            }

            _enableCache.Clear();
            _enableCache.AddRange(this.enables);

            for (int i = 0, count = _enableCache.Count; i < count; i++)
            {
                IAtomicEnable enable = _enableCache[i];
                enable.Enable();
            }
        }

        public void Disable()
        {
            if (this.disables.Count == 0)
            {
                return;
            }

            _disableCache.Clear();
            _disableCache.AddRange(this.disables);

            for (int i = 0, count = _disableCache.Count; i < count; i++)
            {
                IAtomicDisable disable = _disableCache[i];
                disable.Disable();
            }

            _enabled = false;
        }

        public void OnUpdate(float deltaTime)
        {
            if (this.updates.Count == 0)
            {
                return;
            }

            _updateCache.Clear();
            _updateCache.AddRange(this.updates);

            for (int i = 0, count = _updateCache.Count; i < count; i++)
            {
                IAtomicUpdate update = _updateCache[i];
                update.OnUpdate(deltaTime);
            }
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (this.fixedUpdates.Count == 0)
            {
                return;
            }

            _fixedUpdateCache.Clear();
            _fixedUpdateCache.AddRange(this.fixedUpdates);

            for (int i = 0, count = _fixedUpdateCache.Count; i < count; i++)
            {
                IAtomicFixedUpdate fixedUpdate = _fixedUpdateCache[i];
                fixedUpdate.OnFixedUpdate(deltaTime);
            }
        }

        public void OnLateUpdate(float deltaTime)
        {
            if (this.lateUpdates.Count == 0)
            {
                return;
            }

            _lateUpdateCache.Clear();
            _lateUpdateCache.AddRange(this.lateUpdates);

            for (int i = 0, count = _lateUpdateCache.Count; i < count; i++)
            {
                IAtomicLateUpdate lateUpdate = _lateUpdateCache[i];
                lateUpdate.OnLateUpdate(deltaTime);
            }
        }
    }
}