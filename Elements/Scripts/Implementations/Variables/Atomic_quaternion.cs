#if UNITY_MATHEMATICS
using System;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public class Atomic_quaternion : IAtomicVariableObservable<quaternion>
    {
        private Action<quaternion> onChanged;

        [SerializeField]
        private quaternion value;

        public quaternion Value
        {
            get { return this.value; }
            set
            {
                if (math.any(this.value.value != value.value))
                {
                    this.value = value;
                    this.onChanged?.Invoke(value);
                }
            }
        }

        public Atomic_quaternion()
        {
            this.value = default;
        }

        public Atomic_quaternion(quaternion value)
        {
            this.value = value;
        }

        public void Subscribe(Action<quaternion> listener)
        {
            this.onChanged += listener;
        }

        public void Unsubscribe(Action<quaternion> listener)
        {
            this.onChanged -= listener;
        }

#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(OnValueChanged))]
#endif
        private void OnValueChanged(quaternion value)
        {
            this.onChanged?.Invoke(value);
        }
    }
}
#endif