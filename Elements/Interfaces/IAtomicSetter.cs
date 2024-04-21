namespace Atomic.Elements
{
    public interface IAtomicSetter<in T>
    {
        T Value { set; }
    }
}