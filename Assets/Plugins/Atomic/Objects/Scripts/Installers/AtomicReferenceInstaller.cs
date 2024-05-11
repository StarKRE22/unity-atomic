using System;
using Atomic.Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Objects
{
    [Serializable, InlineProperty]
    public class AtomicReferenceInstaller<T> : IAtomicObject.IComposable
    {
        [ReferenceId]
        [HorizontalGroup]
        [SerializeField]
        private int id = -1;
        
        [HideLabel]
        [HorizontalGroup]
        [SerializeField]
        protected T value;

        public T Value => this.value;
        
        public AtomicReferenceInstaller()
        {
        }

        public AtomicReferenceInstaller(T value)
        {
            this.value = value;
        }

        public virtual void Compose(IAtomicObject obj)
        {
            obj.AddReference(this.id, this.value);
        }
    }
}