using System;
using System.Collections.Generic;
using System.Linq;
using AIModule;
using UnityEngine;
// ReSharper disable CollectionNeverQueried.Global

namespace GOAPModule
{
    [Serializable]
    public abstract class GoapAction : ScriptableObject, ISerializationCallbackReceiver
    {
        protected internal enum State
        {
            RUNNING = 0,
            SUCCESS = 1,
            FAILURE = 2
        }
        
        internal IReadOnlyDictionary<ushort, bool> postConditions;
        internal IReadOnlyDictionary<ushort, bool> preConditions;
        
        [SerializeField] private GoapCondition[] _postConditions;
        [SerializeField] private GoapCondition[] _preConditions;

        private bool enable;
        
        protected internal abstract bool IsValid(IBlackboard blackboard);
        
        protected internal abstract int GetCost(IBlackboard blackboard);
        
        internal State Run(IBlackboard blackboard, float deltaTime)
        {
            if (!this.enable)
            {
                this.enable = true;
                this.OnStart(blackboard);
            }

            State result = this.OnUpdate(blackboard, deltaTime);

            if (result != State.RUNNING)
            {
                this.enable = false;
                this.OnStop(blackboard);
            }

            return result;
        }

        internal void Cancel(IBlackboard blackboard)
        {
            if (this.enable)
            {
                this.enable = false;
                this.OnCancel(blackboard);
                this.OnStop(blackboard);
            }
        }
        
        protected abstract State OnUpdate(IBlackboard blackboard, float deltaTime);

        protected virtual void OnStart(IBlackboard blackboard)
        {
        }

        protected virtual void OnStop(IBlackboard blackboard)
        {
        }
        
        protected virtual void OnCancel(IBlackboard blackboard)
        {
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.OnValidate();
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

        private void OnValidate()
        {
            this.postConditions = _postConditions.ToDictionary(it => it.key, it => it.value);
            this.preConditions = _preConditions.ToDictionary(it => it.key, it => it.value);
        }
    }
}