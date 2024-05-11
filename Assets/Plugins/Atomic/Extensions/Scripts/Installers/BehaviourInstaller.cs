using System;
using Atomic.Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic
{
    [Serializable, InlineProperty]
    public class BehaviourInstaller<T> : IAtomicObject.IComposable where T : IAtomicObject.IBehaviour
    {
        [SerializeField]
        protected T value;

        public T Value => this.value;
        
        public BehaviourInstaller()
        {
        }

        public BehaviourInstaller(T value)
        {
            this.value = value;
        }

        public virtual void Compose(IAtomicObject obj)
        {
            obj.AddBehaviour(this.value);
        }
    }
}