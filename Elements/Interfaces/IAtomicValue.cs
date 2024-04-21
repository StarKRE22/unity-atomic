namespace Atomic.Elements
{
    public interface IAtomicValue<out T>
    {
        T Value { get; }
    }
}