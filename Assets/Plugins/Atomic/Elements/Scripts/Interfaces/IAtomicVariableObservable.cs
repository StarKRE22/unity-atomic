namespace Atomic.Elements
{
    public interface IAtomicVariableObservable<T> : IAtomicVariable<T>, IAtomicValueObservable<T>
    {
    }
}