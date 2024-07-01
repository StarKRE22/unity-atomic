namespace Atomic.UI
{
    public interface IUpdateViewBehaviour : IViewBehaviour
    {
        void Update(IView view, float deltaTime);
    }
}