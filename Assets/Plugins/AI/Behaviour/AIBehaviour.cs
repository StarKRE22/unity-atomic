using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper stoppable ConvertToAutoPropertyWithPrivateSetter
// ReSharper stoppable FieldCanBeMadeReadOnly.Local
// ReSharper disable ConvertToAutoPropertyWithPrivateSetter

namespace AIModule
{
    public sealed class AIBehaviour : MonoBehaviour, ISerializationCallbackReceiver
    {
        [ShowInInspector, ReadOnly, HideInEditorMode, PropertyOrder(-10)]
        public bool IsStarted => this.isStarted;

        [SerializeField]
        private Blackboard blackboard;

        [Header("Initial Mechanics")]
        [SerializeField, HideInPlayMode]
        private AILogic[] scriptableMechanics = Array.Empty<AILogic>();

        [SerializeReference, HideInPlayMode]
        private IAILogic[] plainMechanics = Array.Empty<IAILogic>();

        [ShowInInspector, ReadOnly, HideInEditorMode]
        private List<IAILogic> allLogics = new();

        private List<IAIStartable> startables;
        private List<IAIStoppable> stoppables;
        private List<IAIUpdatable> updatables;

        private List<IAIStartable> _startableCache = new();
        private List<IAIStoppable> _stoppableCache = new();
        private List<IAIUpdatable> _updatableCache = new();

        private bool isStarted;

        [Title("Debug")]
        [Button, HideInEditorMode]
        public void OnStart()
        {
            this.isStarted = true;

            if (this.startables.Count == 0)
            {
                return;
            }

            _startableCache.Clear();
            _startableCache.AddRange(this.startables);

            for (int i = 0, count = _startableCache.Count; i < count; i++)
            {
                IAIStartable startable = _startableCache[i];
                startable.OnStart(this.blackboard);
            }
        }

        [Button, HideInEditorMode]
        public void OnUpdate(float deltaTime)
        {
            if (this.updatables.Count == 0)
            {
                return;
            }

            _updatableCache.Clear();
            _updatableCache.AddRange(this.updatables);

            for (int i = 0, count = _updatableCache.Count; i < count; i++)
            {
                IAIUpdatable logic = _updatableCache[i];
                logic.OnUpdate(this.blackboard, deltaTime);
            }
        }

        [Button, HideInEditorMode]
        public void OnStop()
        {
            if (this.stoppables.Count == 0)
            {
                return;
            }

            _stoppableCache.Clear();
            _stoppableCache.AddRange(this.stoppables);

            for (int i = 0, count = _stoppableCache.Count; i < count; i++)
            {
                IAIStoppable stoppable = _stoppableCache[i];
                stoppable.OnStop(this.blackboard);
            }

            this.isStarted = false;
        }

        public void OnGizmos()
        {
            if (this.blackboard == null)
            {
                return;
            }

            for (int i = 0, count = this.allLogics.Count; i < count; i++)
            {
                IAILogic logic = this.allLogics[i];
                if (logic is IAIGizmos gizmos)
                {
                    gizmos.OnGizmos(this.blackboard);
                }
            }
        }

        [Button, HideInEditorMode]
        public void AddLogic(IAILogic target)
        {
            if (target == null)
            {
                return;
            }

            this.allLogics.Add(target);

            if (target is IAIStartable startable)
            {
                this.startables.Add(startable);

                if (this.isStarted)
                {
                    startable.OnStart(this.blackboard);
                }
            }

            if (target is IAIStoppable stoppable)
            {
                this.stoppables.Add(stoppable);
            }

            if (target is IAIUpdatable update)
            {
                this.updatables.Add(update);
            }
        }

        [Button, HideInEditorMode]
        public void RemoveLogic(IAILogic target)
        {
            if (target == null)
            {
                return;
            }

            if (!this.allLogics.Remove(target))
            {
                return;
            }

            if (target is IAIStartable startable)
            {
                this.startables.Remove(startable);
            }

            if (target is IAIUpdatable updatable)
            {
                this.updatables.Remove(updatable);
            }

            if (target is IAIStoppable stoppable)
            {
                if (this.isStarted)
                {
                    stoppable.OnStop(this.blackboard);
                }
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.allLogics = new List<IAILogic>();
            this.startables = new List<IAIStartable>();
            this.stoppables = new List<IAIStoppable>();
            this.updatables = new List<IAIUpdatable>();

            for (int i = 0, count = this.scriptableMechanics.Length; i < count; i++)
            {
                AILogic logic = this.scriptableMechanics[i];
                this.AddLogic(logic);
            }

            for (int i = 0, count = this.plainMechanics.Length; i < count; i++)
            {
                IAILogic logic = this.plainMechanics[i];
                this.AddLogic(logic);
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        private void OnValidate()
        {
            if (EditorApplication.isPlaying)
            {
                return;
            }
            
            
            for (int i = 0, count = this.plainMechanics.Length; i < count; i++)
            {
                IAILogic logic = this.plainMechanics[i];
                if (logic is IAIValidate validate)
                {
                    validate.OnValidate();
                }
                
                this.AddLogic(logic);
            }
        }
    }
}