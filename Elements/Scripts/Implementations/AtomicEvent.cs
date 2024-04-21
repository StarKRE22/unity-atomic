using System;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    
    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public class AtomicEvent : IAtomicEvent, IDisposable
    {
        private Action onEvent;

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
        public virtual void Invoke()
        {
            this.onEvent?.Invoke();
        }
        
        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.onEvent);
        }
    }

    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public class AtomicEvent<T> : IAtomicEvent<T>, IDisposable
    {
        private Action<T> onEvent;
        
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
        public virtual void Invoke(T arg)
        {
            this.onEvent?.Invoke(arg);
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.onEvent);
        }
    }
    
    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public class AtomicEvent<T1, T2> : IAtomicEvent<T1, T2>, IDisposable
    {
        private Action<T1, T2> onEvent;
        
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
        public virtual void Invoke(T1 args1, T2 args2)
        {
            this.onEvent?.Invoke(args1, args2);
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.onEvent);
        }
    }
    
    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public class AtomicEvent<T1, T2, T3> : IAtomicEvent<T1, T2, T3>, IDisposable
    {
        private Action<T1, T2, T3> onEvent;
        
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
        public virtual void Invoke(T1 args1, T2 args2, T3 args3)
        {
            this.onEvent?.Invoke(args1, args2, args3);
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.onEvent);
        }
    }
}