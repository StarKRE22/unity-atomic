namespace Atomic
{
    public interface IAtomicValue<out T>
    {
        T Value { get; }
    }
}