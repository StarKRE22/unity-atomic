namespace Atomic.UI
{
    public interface IUpdateHandler : IHandler
    {
        void Update(IView view, float deltaTime);
    }
}