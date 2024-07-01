using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper stoppable ConvertToAutoPropertyWithPrivateSetter
// ReSharper stoppable FieldCanBeMadeReadOnly.Local
// ReSharper disable ConvertToAutoPropertyWithPrivateSetter

namespace Modules.AI
{
    [AddComponentMenu("Modules/AI/AI Behaviour Group")]
    [DisallowMultipleComponent]
    public sealed class SceneBehaviourGroup : MonoBehaviour, ISerializationCallbackReceiver
    {
        [HideInPlayMode]
        [SerializeField]
        private SceneBlackboard blackboard;

        [SerializeReference, HideInPlayMode]
        private IBehaviour[] initialBehaviours = Array.Empty<IBehaviour>();

        private BehaviourGroup behaviourGroup;

        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly, HideInEditorMode]
        public bool IsStarted => behaviourGroup.IsStarted;

        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly, HideInEditorMode]
        public IReadOnlyList<IBehaviour> AllBehaviours => behaviourGroup.AllBehaviours;

        [FoldoutGroup("Debug")]
        [Button, HideInEditorMode]
        public void OnStart()
        {
            this.behaviourGroup.OnStart();
        }

        [FoldoutGroup("Debug")]
        [Button, HideInEditorMode]
        public void OnUpdate(float deltaTime)
        {
            this.behaviourGroup.OnUpdate(deltaTime);
        }
        
        [FoldoutGroup("Debug")]
        [Button, HideInEditorMode]
        public void OnStop()
        {
            this.behaviourGroup.OnStop();
        }

        [FoldoutGroup("Debug")]
        [Button, HideInEditorMode]
        public bool AddBehaviour(IBehaviour target)
        {
            return behaviourGroup.AddBehaviour(target);
        }

        [FoldoutGroup("Debug")]
        [Button, HideInEditorMode]
        public bool DelBehaviour(IBehaviour target)
        {
            return behaviourGroup.DelBehaviour(target);
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.behaviourGroup = new BehaviourGroup(this.blackboard);

            for (int i = 0, count = this.initialBehaviours.Length; i < count; i++)
            {
                this.behaviourGroup.AddBehaviour(this.initialBehaviours[i]);
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        #region Unity

        [PropertyOrder(-100)]
        [SerializeField]
        private bool controlState = true;

        [PropertyOrder(-99)]
        [ShowIf(nameof(controlState))]
        [SerializeField]
        private UpdateMode updateMode = UpdateMode.UPDATE;
        
        private void Awake()
        {
            if (!this.controlState)
            {
                this.enabled = false;
            }
        }

        private void OnEnable()
        {
            if (this.controlState)
            {
                this.behaviourGroup.OnStart();
            }
        }
        
        private void OnDisable()
        {
            if (this.controlState)
            {
                this.behaviourGroup.OnStop();
            }
        }

        private void Update()
        {
            if (this.controlState && this.updateMode == UpdateMode.UPDATE)
            {
                this.behaviourGroup.OnUpdate(Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (this.controlState && this.updateMode == UpdateMode.FIXED_UPDATE)
            {
                this.behaviourGroup.OnUpdate(Time.fixedDeltaTime);
            }
        }

        #endregion
    }
}