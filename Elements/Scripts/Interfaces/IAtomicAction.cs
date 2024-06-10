namespace Atomic.Elements
{
    public interface IAtomicAction
    {
        void Invoke();
    }

    public interface IAtomicAction<in T>
    {
        void Invoke(T arg);
    }

    public interface IAtomicAction<in T1, in T2>
    {
        void Invoke(T1 args1, T2 args2);
    }
    
    public interface IAtomicAction<in T1, in T2, in T3>
    {
        void Invoke(T1 args1, T2 args2, T3 args3);
    }

    public interface IAtomicActionOut<O>
    {
        void Invoke(out O result);
    }
    
    public interface IAtomicActionOut<in T, O>
    {
        void Invoke(T arg, out O result);
    }
    
    public interface IAtomicActionOut<in T1, in T2, O>
    {
        void Invoke(T1 arg1, T2 arg2, out O result);
    }

}


