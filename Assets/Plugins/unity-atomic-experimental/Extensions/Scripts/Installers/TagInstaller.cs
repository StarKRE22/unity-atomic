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
    public sealed class TagInstaller : IObjectInstaller
    {
        [TagContract]
        [SerializeField]
        private int tag = -1;

        public int Tag => this.tag;

        public void Install(IObject obj)
        {
            obj.AddTag(this.tag);
        }
    }
}