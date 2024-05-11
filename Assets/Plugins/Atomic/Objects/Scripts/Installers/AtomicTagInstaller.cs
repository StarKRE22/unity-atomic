using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Objects
{
    [Serializable, InlineProperty]
    public sealed class AtomicTagInstaller : IAtomicObject.IComposable
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