using System;
using System.Collections.Generic;
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
    public class AtomicVariable<T> : IAtomicVariableObservable<T>, IDisposable
    {
        private static readonly IEqualityComparer<T> equalityComparer = EqualityComparer.GetDefault<T>();

        public T Value
        {
            get { return this.value; }
            set
            {
                if (!equalityComparer.Equals(this.value, value))
                {
                    this.value = value;
                    this.onChanged?.Invoke(value);
                }
            }
        }

        public void Subscribe(Action<T> listener)
        {
            this.onChanged += listener;
        }

        public void Unsubscribe(Action<T> listener)
        {
            this.onChanged -= listener;
        }

        private Action<T> onChanged;

        [SerializeField]
#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(OnValueChanged))]
#endif
        private T value;

        public AtomicVariable()
        {
            this.value = default;
        }

        public AtomicVariable(T value)
        {
            this.value = value;
        }

        private void OnValueChanged(T value)
        {
            this.onChanged?.Invoke(value);
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.onChanged);
        }
    }
}