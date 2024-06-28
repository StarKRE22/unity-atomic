namespace Atomic.UI
{
    public interface IAwakeHandler : IHandler
    {
        void Awake(IView view);
    }
}