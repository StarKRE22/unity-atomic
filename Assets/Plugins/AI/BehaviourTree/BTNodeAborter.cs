using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIModule
{
    [Serializable]
    public sealed class BTNodeAborter : BTNode, ISerializationCallbackReceiver, IBTNodeParent
    {
        public override string Name => this.node != null ? this.node.Name : "Undefined";

        [Header("Condition")]
        [SerializeReference, Space]
        private IAICondition[] conditions;

        [Header("Node")]
        [SerializeReference, Space]
        private BTNode node;

        [SerializeField, HideInInspector]
        private bool[] states;

        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            this.TryAbort(blackboard);
            return this.node.Run(blackboard, deltaTime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void TryAbort(IBlackboard blackboard)
        {
            int count = this.conditions.Length;
            
            for (int i = 0; i < count; i++)
            {
                bool prevState = this.states[i];
                bool newState = this.conditions[i].Check(blackboard);
                
                if (newState != prevState)
                {
                    this.states[i] = newState;
                    this.node.Abort(blackboard);
                    return;
                }
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.conditions ??= Array.Empty<IAICondition>();
            this.states = new bool[this.conditions.Length];
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        public bool FindChild(string name, out BTNode result)
        {
            if (name.Equals(this.node.Name))
            {
                result = this.node;
                return true;
            }

            if (this.node is IBTNodeParent parent && parent.FindChild(name, out result))
            {
                return true;
            }

            result = default;
            return false;
        }
    }
}