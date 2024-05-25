namespace Atomic.Objects
{
    public interface IComposer
    {
        void Compose(IAtomicObject obj);
    }

    public interface IDisposer
    {
        void Dispose(IAtomicObject obj);
    }

    public interface IAspect : IComposer, IDisposer
    {
    }
}