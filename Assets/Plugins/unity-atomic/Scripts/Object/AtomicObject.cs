using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

// ReSharper disable FieldCanBeMadeReadOnly.Local

#if ODIN_INSPECTOR
#endif

namespace Atomic
{
    [AddComponentMenu("Atomic/Atomic Object")]
    [DisallowMultipleComponent]
    public class AtomicObject : AtomicEntity, IAtomicObject,
        IAtomicEnable,
        IAtomicDisable,
        IAtomicUpdate,
        IAtomicFixedUpdate,
        IAtomicLateUpdate,
#if UNITY_EDITOR
        IAtomicDrawGizmos
#endif
    {
        public bool Enabled => _enabled;

#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly, PropertySpace(8), PropertyOrder(100)]
#endif
        private List<IAtomicLogic> allLogics;

        private List<IAtomicEnable> enables;
        private List<IAtomicDisable> disables;
        private List<IAtomicUpdate> updates;
        private List<IAtomicFixedUpdate> fixedUpdates;
        private List<IAtomicLateUpdate> lateUpdates;

#if UNITY_EDITOR
        private readonly List<IAtomicDrawGizmos> drawGizmoses;
#endif

        private readonly List<IAtomicEnable> _enableCache = new();
        private readonly List<IAtomicDisable> _disableCache = new();
        private readonly List<IAtomicUpdate> _updateCache = new();
        private readonly List<IAtomicFixedUpdate> _fixedUpdateCache = new();
        private readonly List<IAtomicLateUpdate> _lateUpdateCache = new();

        private bool _enabled;

        public IAtomicLogic[] AllLogic()
        {
            return this.allLogics.ToArray();
        }

        public IReadOnlyList<IAtomicLogic> AllLogicReadOnly()
        {
            return this.allLogics;
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

        public IList<IAtomicLogic> GetAllLogicRaw()
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

#if UNITY_EDITOR
        public void OnGizmosDraw()
        {
            foreach (IAtomicDrawGizmos gizmos in this.drawGizmoses)
            {
                gizmos.OnGizmosDraw();
            }
        }
#endif

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

#if UNITY_EDITOR
            if (target is IAtomicDrawGizmos gizmos)
            {
                this.drawGizmoses.Add(gizmos);
            }
#endif
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

#if UNITY_EDITOR
            if (target is IAtomicDrawGizmos gizmos)
            {
                this.drawGizmoses.Remove(gizmos);
            }
#endif

            if (target is IAtomicDisable disable)
            {
                if (_enabled)
                {
                    disable.Disable();
                }
            }
        }

        #region Internal

        [FoldoutGroup("Installers")]
        [PropertyOrder(80)]
        [SerializeReference]
        private IInstaller[] objectInstallers = default;

        public new interface IInstaller
        {
            void Install(AtomicObject obj);
        }

        private protected override void Compose()
        {
            base.Compose();

            _enabled = false;

            this.allLogics = new List<IAtomicLogic>();
            this.enables = new List<IAtomicEnable>();
            this.disables = new List<IAtomicDisable>();
            this.updates = new List<IAtomicUpdate>();
            this.fixedUpdates = new List<IAtomicFixedUpdate>();
            this.lateUpdates = new List<IAtomicLateUpdate>();

#if UNITY_EDITOR
            List<IAtomicDrawGizmos> drawGizmoses = new List<IAtomicDrawGizmos>();
#endif

            if (this.objectInstallers is {Length: > 0})
            {
                for (int i = 0, count = this.objectInstallers.Length; i < count; i++)
                {
                    IInstaller installer = this.objectInstallers[i];
                    if (installer != null)
                    {
                        installer.Install(this);
                    }
                }
            }
        }

        #endregion
    }
}