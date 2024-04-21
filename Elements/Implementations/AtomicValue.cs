using System;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public sealed class AtomicValue<T> : IAtomicValue<T>
    {
        public T Value
        {
            get { return this.value; }
        }

        [SerializeField]
#if ODIN_INSPECTOR
        [HideLabel]
#endif
        private T value;

        public AtomicValue(T value)
        {
            this.value = value;
        }
    }
}