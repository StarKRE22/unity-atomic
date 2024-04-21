using System;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    [Serializable]
    public class AtomicFunction<T> : IAtomicFunction<T>
    {
        private Func<T> func;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        public T Value
        {
            get { return this.func != null ? this.func.Invoke() : default; }
        }

        public AtomicFunction()
        {
        }

        public AtomicFunction(Func<T> func)
        {
            this.func = func;
        }

        public void Compose(Func<T> func)
        {
            this.func = func;
        }

        public T Invoke()
        {
            return this.func != null ? this.func.Invoke() : default;
        }
    }

    [Serializable]
    public sealed class AtomicFunction<T, R> : IAtomicFunction<T, R>
    {
        private Func<T, R> func;

        public AtomicFunction()
        {
        }

        public AtomicFunction(Func<T, R> func)
        {
            this.func = func;
        }

        public void Compose(Func<T, R> func)
        {
            this.func = func;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public R Invoke(T args)
        {
            return this.func.Invoke(args);
        }
    }
}