namespace Atomic.UI
{
    public interface IUpdate
    {
        void OnUpdate(IView view, float deltaTime);
    }
}