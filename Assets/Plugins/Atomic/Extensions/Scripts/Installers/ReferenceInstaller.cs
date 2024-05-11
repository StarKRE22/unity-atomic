using System;
using Atomic.Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic
{
    [Serializable, InlineProperty]
    public class ReferenceInstaller<T> : IAtomicObject.IComposable
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
        
        public ReferenceInstaller()
        {
        }

        public ReferenceInstaller(T value)
        {
            this.value = value;
        }

        public virtual void Compose(IAtomicObject obj)
        {
            obj.AddReference(this.id, this.value);
        }
    }
}