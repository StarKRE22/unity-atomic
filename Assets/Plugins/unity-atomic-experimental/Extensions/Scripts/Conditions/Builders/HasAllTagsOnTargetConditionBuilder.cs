using System;
using Atomic.Contracts;
using Atomic.Extensions;
using Atomic.Objects;
using UnityEngine;

namespace Atomic.Extensions
{
    [Serializable]
    public sealed class HasAllTagsOnTargetConditionBuilder : ITargetConditionBuilder
    {
        [TagContract]
        [SerializeField]
        private int[] tags;
        
        public Func<IObject, bool> Build(IObject obj)
        {
            return target => target.HasAllTags(this.tags);
        }
    }
}