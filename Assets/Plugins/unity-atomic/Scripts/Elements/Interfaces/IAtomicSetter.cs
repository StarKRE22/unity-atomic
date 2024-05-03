namespace Atomic
{
    public interface IAtomicSetter<in T>
    {
        T Value { set; }
    }
}