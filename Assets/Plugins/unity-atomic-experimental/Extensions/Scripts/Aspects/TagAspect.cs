using System;
using Atomic.Contracts;
using Atomic.Objects;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Extensions
{
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public sealed class TagAspect : IAspect
    {
        [TagContract]
        [SerializeField]
        private int tag = -1;

        public int Tag => this.tag;

        public void Apply(IObject obj)
        {
            obj.AddTag(this.tag);
        }

        public void Discard(IObject obj)
        {
            obj.DelTag(this.tag);
        }
    }
}