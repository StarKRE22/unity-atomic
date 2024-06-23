namespace Atomic.Elements
{
    public interface IAtomicReference<T>
    {
        ref T Value { get; }
    }
}