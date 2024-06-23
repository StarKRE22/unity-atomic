#if UNITY_MATHEMATICS
using System;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public class Atomic_float3 : IAtomicVariableObservable<float3>
    {
        private Action<float3> onChanged;

        [SerializeField]
        private float3 value;

        public float3 Value
        {
            get { return this.value; }
            set
            {
                if (math.any(this.value != value))
                {
                    this.value = value;
                    this.onChanged?.Invoke(value);
                }
            }
        }

        public Atomic_float3()
        {
            this.value = default;
        }

        public Atomic_float3(float3 value)
        {
            this.value = value;
        }

        public void Subscribe(Action<float3> listener)
        {
            this.onChanged += listener;
        }

        public void Unsubscribe(Action<float3> listener)
        {
            this.onChanged -= listener;
        }

#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(OnValueChanged))]
#endif
        private void OnValueChanged(float3 value)
        {
            this.onChanged?.Invoke(value);
        }
    }
}
#endif