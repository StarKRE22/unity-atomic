using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// Represents a serialized read-only property.
    
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    
    [Serializable]
    public class AtomicValue<T> : IAtomicValue<T>
    {
        public T Value
        {
            get { return this.value; }
        }

#if ODIN_INSPECTOR
        [HideLabel]
#endif
        [SerializeField]
        private T value;

        public AtomicValue()
        {
        }

        public AtomicValue(T value)
        {
            this.value = value;
        }
    }
}
