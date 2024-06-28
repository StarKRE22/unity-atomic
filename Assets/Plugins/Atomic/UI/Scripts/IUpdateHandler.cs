namespace Atomic.UI
{
    public interface IUpdateHandler
    {
        void Update(IView view, float deltaTime);
    }
}