using System;
using Atomic.Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic
{
    [Serializable, InlineProperty]
    public sealed class TagInstaller : IAtomicObject.IComposable
    {
        [TagId]
        [SerializeField]
        private int tag = -1;

        public int Tag => this.tag;

        public void Compose(IAtomicObject obj)
        {
            obj.AddTag(this.tag);
        }
    }
}