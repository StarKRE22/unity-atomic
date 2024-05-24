namespace Atomic.Objects
{
    public interface IComposable
    {
        void Compose(IObject obj);
    }
    
    public interface IDisposable
    {
        void Dispose(IObject obj);
    }
    
    public interface IAspect : IComposable, IDisposable
    {
    }
}