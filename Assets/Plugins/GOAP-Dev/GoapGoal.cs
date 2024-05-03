using System.Collections.Generic;
using System.Linq;
using AIModule;
using UnityEngine;
// ReSharper disable CollectionNeverQueried.Global

namespace GOAPModule
{
    public abstract class GoapGoal : ScriptableObject, ISerializationCallbackReceiver
    {
        internal IReadOnlyDictionary<ushort, bool> postConditions;

        [SerializeField] private GoapCondition[] _postConditions;

        protected internal abstract bool IsValid(IBlackboard blackboard);
        
        protected internal abstract int GetPriority(IBlackboard blackboard);

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
        }
    }
}