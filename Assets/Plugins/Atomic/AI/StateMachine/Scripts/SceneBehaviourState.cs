using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.AI
{
    [AddComponentMenu("Modules/AI/AI Behaviour State")]
    public sealed class SceneBehaviourState : MonoBehaviour, ISerializationCallbackReceiver
    {
        [SerializeField, Space]
        private SceneBlackboard blackboard;

        [Header("Root")]
        [SerializeReference, HideInPlayMode]
        private IState initialState;

        private BehaviourState behaviourState;
        
        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly, HideInEditorMode]
        public IState CurrentState => this.behaviourState?.CurrentState;

        [FoldoutGroup("Debug")]
        [Button, HideInEditorMode]
        public void SwitchState(IState state) => this.behaviourState.SwitchState(state);

        public void OnEnter()
        {
            this.behaviourState.OnEnter();
        }

        public void OnExit()
        {
            this.behaviourState.OnExit();
        }

        public void OnUpdate(float deltaTime)
        {
            this.behaviourState.OnUpdate(deltaTime);
        }

        #region Unity
        
        [PropertyOrder(-100)]
        [SerializeField]
        private bool controlState = true;

        [PropertyOrder(-99)]
        [ShowIf(nameof(controlState))]
        [SerializeField]
        private UpdateMode updateMode = UpdateMode.UPDATE;

        private enum UpdateMode
        {
            UPDATE = 0,
            FIXED_UPDATE = 1,
            MANUAL = 2,
        }

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
                this.behaviourState.OnEnter();
            }
        }

        private void OnDisable()
        {
            if (this.controlState)
            {
                this.behaviourState.OnExit();
            }
        }

        private void Update()
        {
            if (this.controlState && this.updateMode == UpdateMode.UPDATE)
            {
                this.behaviourState.OnUpdate(Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (this.controlState && this.updateMode == UpdateMode.FIXED_UPDATE)
            {
                this.behaviourState.OnUpdate(Time.fixedDeltaTime);
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.behaviourState = new BehaviourState(this.blackboard, this.initialState);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }
        
        #endregion
    }
}