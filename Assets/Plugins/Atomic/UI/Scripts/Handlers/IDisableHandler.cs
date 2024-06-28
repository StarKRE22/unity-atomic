namespace Atomic.UI
{
    public interface IDisableHandler : IHandler
    {
        void Disable(IView view);
    }
}