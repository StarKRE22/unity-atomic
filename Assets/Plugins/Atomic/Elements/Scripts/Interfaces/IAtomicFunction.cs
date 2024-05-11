namespace Atomic.Elements
{
    public interface IAtomicFunction<out R> : IAtomicValue<R>
    {
        R Invoke();

        R IAtomicValue<R>.Value => this.Invoke();
    }

    public interface IAtomicFunction<in T, out R>
    {
        R Invoke(T args);
    }
    
    public interface IAtomicFunction<in T1, in T2, out R>
    {
        R Invoke(T1 args1, T2 args2);
    }
}