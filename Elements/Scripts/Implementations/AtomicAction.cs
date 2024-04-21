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
    public class AtomicAction : IAtomicAction
    {
        private Action action;

        public AtomicAction()
        {
        }

        public AtomicAction(Action action)
        {
            this.action = action;
        }

        public void Compose(Action action)
        {
            this.action = action;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke()
        {
            this.action?.Invoke();
        }
    }

    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public class AtomicAction<T> : IAtomicAction<T>
    {
        private Action<T> action;

        public AtomicAction()
        {
        }

        public AtomicAction(Action<T> action)
        {
            this.action = action;
        }
        
        public void Compose(Action<T> action)
        {
            this.action = action;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T arg)
        {
            this.action?.Invoke(arg);
        }
    }
    
    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public class AtomicAction<T1, T2> : IAtomicAction<T1, T2>
    {
        private Action<T1, T2> action;

        public AtomicAction()
        {
        }

        public AtomicAction(Action<T1, T2> action)
        {
            this.action = action;
        }
        
        public void Compose(Action<T1, T2> action)
        {
            this.action = action;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T1 args1, T2 args2)
        {
            this.action?.Invoke(args1, args2);
        }
    }
    
    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public class AtomicAction<T1, T2, T3> : IAtomicAction<T1, T2, T3>
    {
        private Action<T1, T2, T3> action;

        public AtomicAction()
        {
        }

        public AtomicAction(Action<T1, T2, T3> action)
        {
            this.action = action;
        }
        
        public void Compose(Action<T1, T2, T3> action)
        {
            this.action = action;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T1 args1, T2 args2, T3 args3)
        {
            this.action?.Invoke(args1, args2, args3);
        }
    }
}