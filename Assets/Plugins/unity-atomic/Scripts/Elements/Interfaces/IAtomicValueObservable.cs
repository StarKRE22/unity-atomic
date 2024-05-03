namespace Atomic
{
    public interface IAtomicValueObservable<out T> : IAtomicValue<T>, IAtomicObservable<T>
    {
    }
}