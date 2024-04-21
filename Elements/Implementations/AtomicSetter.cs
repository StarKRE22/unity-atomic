using System;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    [Serializable]
    public class AtomicSetter<T> : IAtomicSetter<T>
    {
        public T Value
        {
            set => this.action?.Invoke(value);
        }

        private Action<T> action;

        public AtomicSetter()
        {
        }

        public AtomicSetter(Action<T> action)
        {
            this.action = action;
        }

        public void Compose(Action<T> action)
        {
            this.action = action;
        }

#if UNITY_EDITOR
#if ODIN_INSPECTOR
        [Button("Set Value")]
#endif
        private void SetValueEditor(T value) => this.action?.Invoke(value);
#endif
    }
}