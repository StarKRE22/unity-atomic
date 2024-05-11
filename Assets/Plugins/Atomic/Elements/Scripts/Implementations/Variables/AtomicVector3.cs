using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    [Serializable]
    public class AtomicVector3 : IAtomicVariableObservable<Vector3>, IDisposable
    {
        private Action<Vector3> onChanged;
        
        [SerializeField]
        private Vector3 value;

        public Vector3 Value
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

        public AtomicVector3()
        {
            this.value = default;
        }

        public AtomicVector3(Vector3 value)
        {
            this.value = value;
        }

        public void Subscribe(Action<Vector3> listener)
        {
            this.onChanged += listener;
        }

        public void Unsubscribe(Action<Vector3> listener)
        {
            this.onChanged -= listener;
        }

#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(OnValueChanged))]
#endif
        private void OnValueChanged(Vector3 value)
        {
            this.onChanged?.Invoke(value);
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.onChanged);
        }
    }
}