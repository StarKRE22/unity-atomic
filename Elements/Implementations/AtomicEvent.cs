using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// Represents an event object 

#if ODIN_INSPECTOR
    [InlineProperty]
#endif

    [Serializable]
    public class AtomicEvent : IAtomicEvent, IDisposable
    {
        private event Action onEvent;

        public void Subscribe(Action action)
        {
            this.onEvent += action;
        }

        public void Unsubscribe(Action action)
        {
            this.onEvent -= action;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke()
        {
            this.onEvent?.Invoke();
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.onEvent);
        }
    }

#if ODIN_INSPECTOR
    [InlineProperty]
#endif

    [Serializable]
    public class AtomicEvent<T> : IAtomicEvent<T>, IDisposable
    {
        private event Action<T> onEvent;

        public void Subscribe(Action<T> action)
        {
            this.onEvent += action;
        }

        public void Unsubscribe(Action<T> action)
        {
            this.onEvent -= action;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T arg)
        {
            this.onEvent?.Invoke(arg);
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.onEvent);
        }
    }

#if ODIN_INSPECTOR
    [InlineProperty]
#endif

    [Serializable]
    public class AtomicEvent<T1, T2> : IAtomicEvent<T1, T2>, IDisposable
    {
        private event Action<T1, T2> onEvent;

        public void Subscribe(Action<T1, T2> action)
        {
            this.onEvent += action;
        }

        public void Unsubscribe(Action<T1, T2> action)
        {
            this.onEvent -= action;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T1 args1, T2 args2)
        {
            this.onEvent?.Invoke(args1, args2);
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.onEvent);
        }
    }

#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    
    [Serializable]
    public class AtomicEvent<T1, T2, T3> : IAtomicEvent<T1, T2, T3>, IDisposable
    {
        private event Action<T1, T2, T3> onEvent;

        public void Subscribe(Action<T1, T2, T3> action)
        {
            this.onEvent += action;
        }

        public void Unsubscribe(Action<T1, T2, T3> action)
        {
            this.onEvent -= action;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T1 args1, T2 args2, T3 args3)
        {
            this.onEvent?.Invoke(args1, args2, args3);
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.onEvent);
        }
    }
}
