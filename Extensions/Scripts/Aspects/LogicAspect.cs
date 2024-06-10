using System;
using Atomic.Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Extensions
{
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class LogicAspect<T> : IAspect where T : ILogic
    {
        [SerializeField]
        protected T value;

        public T Value => this.value;

        public LogicAspect()
        {
        }

        public LogicAspect(T value)
        {
            this.value = value;
        }

        public virtual void Apply(IObject obj)
        {
            obj.AddLogic(this.value);
        }

        public void Discard(IObject obj)
        {
            obj.DelLogic(this.value);
        }
    }
}