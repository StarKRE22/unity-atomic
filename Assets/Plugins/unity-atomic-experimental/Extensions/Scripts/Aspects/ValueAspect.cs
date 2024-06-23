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
    public class ValueAspect<T> : IAspect
    {
        [ValueContract]
#if ODIN_INSPECTOR
        [HorizontalGroup]
#endif
        [SerializeField]
        private int id = -1;

#if ODIN_INSPECTOR
        [HideLabel]
        [HorizontalGroup]
#endif
        [SerializeField]
        protected T value;

        public T Value => this.value;

        public ValueAspect()
        {
        }

        public ValueAspect(T value)
        {
            this.value = value;
        }

        public virtual void Apply(IObject obj)
        {
            obj.AddValue(this.id, this.value);
        }

        public virtual void Discard(IObject obj)
        {
            obj.DelValue(this.id);
        }
    }
}