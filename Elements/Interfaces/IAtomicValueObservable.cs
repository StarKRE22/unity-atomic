namespace Atomic.Elements
{
    public interface IAtomicValueObservable<out T> : IAtomicValue<T>, IAtomicObservable<T>
    {
    }
}