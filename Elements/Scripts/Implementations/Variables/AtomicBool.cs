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
    public class AtomicBool : IAtomicVariableObservable<bool>, IDisposable
    {
        private Action<bool> onChanged;
        
        [SerializeField]
        private bool value;

        public bool Value
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

        public AtomicBool()
        {
            this.value = default;
        }

        public AtomicBool(bool value)
        {
            this.value = value;
        }

        public void Subscribe(Action<bool> listener)
        {
            this.onChanged += listener;
        }

        public void Unsubscribe(Action<bool> listener)
        {
            this.onChanged -= listener;
        }

#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(OnValueChanged))]
#endif
        private void OnValueChanged(bool value)
        {
            this.onChanged?.Invoke(value);
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.onChanged);
        }
    }
}