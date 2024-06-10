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
    [Serializable]

#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public class ValueInstaller<T> : IObjectInstaller
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

        public ValueInstaller()
        {
        }

        public ValueInstaller(T value)
        {
            this.value = value;
        }

        public virtual void Install(IObject obj)
        {
            obj.AddValue(this.id, this.value);
        }
    }
}