namespace Atomic.Objects
{
    public interface IAtomicAspect
    {
        void Compose(IAtomicObject obj);
        void Dispose(IAtomicObject obj);
    }
}