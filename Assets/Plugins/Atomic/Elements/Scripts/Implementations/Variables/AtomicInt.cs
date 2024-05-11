using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
#if ODIN_INSPECTOR
    [InlineProperty]
#endif

    [Serializable]
    public class AtomicInt : IAtomicVariableObservable<int>, IDisposable
    {
        private Action<int> onChanged;
        
        [SerializeField]
        private int value;

        public int Value
        {
            get { return this.value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    this.onChanged?.Invoke(value);
                }
            }
        }

        public AtomicInt()
        {
            this.value = default;
        }

        public AtomicInt(int value)
        {
            this.value = value;
        }

        public void Subscribe(Action<int> listener)
        {
            this.onChanged += listener;
        }

        public void Unsubscribe(Action<int> listener)
        {
            this.onChanged -= listener;
        }

#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(OnValueChanged))]
#endif
        private void OnValueChanged(int value)
        {
            this.onChanged?.Invoke(value);
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.onChanged);
        }
    }
}
