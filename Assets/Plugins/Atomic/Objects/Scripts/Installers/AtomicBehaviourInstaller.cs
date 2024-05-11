using System;
using Atomic.Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Objects
{
    [Serializable, InlineProperty]
    public class AtomicBehaviourInstaller<T> : IAtomicObject.IComposable where T : IAtomicObject.IBehaviour
    {
        [SerializeField]
        protected T value;

        public T Value => this.value;
        
        public AtomicBehaviourInstaller()
        {
        }

        public AtomicBehaviourInstaller(T value)
        {
            this.value = value;
        }

        public virtual void Compose(IAtomicObject obj)
        {
            obj.AddBehaviour(this.value);
        }
    }
}