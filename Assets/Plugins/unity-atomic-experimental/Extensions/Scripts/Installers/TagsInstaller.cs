using System;
using Atomic.Contracts;
using Atomic.Objects;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Extensions
{
    [MovedFrom(true, "Atomic.Objects", "Atomic.Objects")]

#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public sealed class TagsInstaller : IObjectInstaller
    {
        [TagContract]
        [SerializeField]
        private int[] tags;

        public int[] Tag => this.tags;

        public void Install(IObject obj)
        {
            if (this.tags != null)
            {
                for (int i = 0, count = this.tags.Length; i < count; i++)
                {
                    obj.AddTag(this.tags[i]);
                }
            }
            
        }
    }
}