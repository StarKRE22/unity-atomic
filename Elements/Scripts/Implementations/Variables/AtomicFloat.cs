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
    public class AtomicFloat : IAtomicVariableObservable<float>, IDisposable
    {
        private Action<float> onChanged;
        
        [SerializeField]
        private float value;

        public float Value
        {
            get { return this.value; }
            set
            {
                if (Math.Abs(this.value - value) > float.Epsilon)
                {
                    this.value = value;
                    this.onChanged?.Invoke(value);
                }
            }
        }

        public AtomicFloat()
        {
            this.value = default;
        }

        public AtomicFloat(float value)
        {
            this.value = value;
        }

        public void Subscribe(Action<float> listener)
        {
            this.onChanged += listener;
        }

        public void Unsubscribe(Action<float> listener)
        {
            this.onChanged -= listener;
        }

#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(OnValueChanged))]
#endif
        private void OnValueChanged(float value)
        {
            this.onChanged?.Invoke(value);
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.onChanged);
        }
    }
}