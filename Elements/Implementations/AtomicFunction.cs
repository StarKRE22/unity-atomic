using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// Represents a function object.
    
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
    
    [Serializable]
    public sealed class AtomicFunction<T1, T2, R> : IAtomicFunction<T1, T2, R>
    {
        private Func<T1, T2, R> func;

        public AtomicFunction()
        {
        }

        public AtomicFunction(Func<T1, T2, R> func)
        {
            this.func = func;
        }

        public void Compose(Func<T1, T2, R> func)
        {
            this.func = func;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public R Invoke(T1 arg1, T2 arg2)
        {
            return this.func.Invoke(arg1, arg2);
        }
    }
}
