using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic
{
#if ODIN_INSPECTOR
    [InlineProperty]
#endif

    [Serializable]
    public class AtomicQuaternion : IAtomicVariableObservable<Quaternion>, IDisposable
    {
        private Action<Quaternion> onChanged;
        
        [SerializeField]
        private Quaternion value;

        public Quaternion Value
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

        public AtomicQuaternion()
        {
            this.value = default;
        }

        public AtomicQuaternion(Quaternion value)
        {
            this.value = value;
        }

        public void Subscribe(Action<Quaternion> listener)
        {
            this.onChanged += listener;
        }

        public void Unsubscribe(Action<Quaternion> listener)
        {
            this.onChanged -= listener;
        }

#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(OnValueChanged))]
#endif
        private void OnValueChanged(Quaternion value)
        {
            this.onChanged?.Invoke(value);
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.onChanged);
        }
    }
}