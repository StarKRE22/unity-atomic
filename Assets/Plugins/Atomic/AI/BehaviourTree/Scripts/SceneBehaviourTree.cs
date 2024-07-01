using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.AI
{
    [AddComponentMenu("Modules/AI/AI Behaviour Tree")]
    [DisallowMultipleComponent]
    public sealed class SceneBehaviourTree : MonoBehaviour, IBehaviourTree, ISerializationCallbackReceiver
    {
        [Space]
        [SerializeField]
        private SceneBlackboard blackboard;
        
        [Space]
        [SerializeReference]
        private BTNode initialRoot;
        
        private BehaviourTree behaviourTree;
        
        public void SetBlackboard(IBlackboard blackboard)
        {
            this.behaviourTree.SetBlackboard(blackboard);
        }

        public void SetRoot(BTNode root)
        {
            this.behaviourTree.SetRoot(root);
        }

        public void OnUpdate(float deltaTime)
        {
            this.behaviourTree.OnUpdate(deltaTime);
        }

        public void Abort()
        {
            this.behaviourTree.Abort();
        }

        public bool FindChild(string name, out BTNode result)
        {
            return this.behaviourTree.FindChild(name, out result);
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.behaviourTree = new BehaviourTree(this.initialRoot, this.blackboard);
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

        private void Update()
        {
            if (this.controlState && this.updateMode == UpdateMode.UPDATE)
            {
                this.behaviourTree.OnUpdate(Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (this.controlState && this.updateMode == UpdateMode.FIXED_UPDATE)
            {
                this.behaviourTree.OnUpdate(Time.fixedDeltaTime);
            }
        }

        #endregion
    }
}