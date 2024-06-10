using System;
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
    public class LogicInstaller<T> : IObjectInstaller where T : ILogic
    {
        [SerializeField]
        protected T value;

        public T Value => this.value;

        public LogicInstaller()
        {
        }

        public LogicInstaller(T value)
        {
            this.value = value;
        }

        public virtual void Install(IObject obj)
        {
            obj.AddLogic(this.value);
        }
    }
}