namespace Atomic
{
    public interface IAtomicVariableObservable<T> : IAtomicVariable<T>, IAtomicValueObservable<T>
    {
    }
}