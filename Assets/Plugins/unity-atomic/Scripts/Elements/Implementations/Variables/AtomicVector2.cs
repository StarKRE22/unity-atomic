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
    public class AtomicVector2 : IAtomicVariableObservable<Vector2>, IDisposable
    {
        private Action<Vector2> onChanged;
        
        [SerializeField]
        private Vector2 value;

        public Vector2 Value
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

        public AtomicVector2()
        {
            this.value = default;
        }

        public AtomicVector2(Vector2 value)
        {
            this.value = value;
        }

        public void Subscribe(Action<Vector2> listener)
        {
            this.onChanged += listener;
        }

        public void Unsubscribe(Action<Vector2> listener)
        {
            this.onChanged -= listener;
        }

#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(OnValueChanged))]
#endif
        private void OnValueChanged(Vector2 value)
        {
            this.onChanged?.Invoke(value);
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.onChanged);
        }
    }
}