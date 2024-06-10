using Sirenix.OdinInspector;

namespace Atomic.Elements
{
    public class AtomicActionOut<O> : IAtomicActionOut<O>
    {
        private ActionOut<O> action;

        public AtomicActionOut()
        {
        }

        public AtomicActionOut(ActionOut<O> action)
        {
            this.action = action;
        }
        
        public void Compose(ActionOut<O> action)
        {
            this.action = action;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(out O result)
        {
            this.action.Invoke(out result);
        }
    }
    
    public class AtomicActionOut<T, O> : IAtomicActionOut<T, O>
    {
        private ActionOut<T, O> action;

        public AtomicActionOut()
        {
        }

        public AtomicActionOut(ActionOut<T, O> action)
        {
            this.action = action;
        }
        
        public void Compose(ActionOut<T, O> action)
        {
            this.action = action;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T arg, out O result)
        {
            this.action.Invoke(arg, out result);
        }
    }
    
    
    public class AtomicActionOut<T1, T2, O> : IAtomicActionOut<T1, T2, O>
    {
        private ActionOut<T1, T2, O> action;

        public AtomicActionOut()
        {
        }

        public AtomicActionOut(ActionOut<T1, T2, O> action)
        {
            this.action = action;
        }
        
        public void Compose(ActionOut<T1, T2, O> action)
        {
            this.action = action;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T1 arg1, T2 arg2, out O result)
        {
            this.action.Invoke(arg1, arg2, out result);
        }
    }
}