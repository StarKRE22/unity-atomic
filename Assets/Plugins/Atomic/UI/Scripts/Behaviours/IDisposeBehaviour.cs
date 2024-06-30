namespace Atomic.UI
{
    public interface IDisposeBehaviour : IBehaviour
    {
        void Dispose(IView view);
    }
}