using System;
using Atomic.Contracts;
using Atomic.Objects;
using UnityEngine;

namespace Atomic.Extensions
{
    [Serializable]
    public sealed class HasTagOnTargetConditionBuilder : ITargetConditionBuilder
    {
        [TagContract]
        [SerializeField]
        private int tag;
        
        public Func<IObject, bool> Build(IObject obj)
        {
            return target => target.HasTag(this.tag);
        }
    }
}