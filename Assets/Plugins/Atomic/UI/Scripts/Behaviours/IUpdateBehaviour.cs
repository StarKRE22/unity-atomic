namespace Atomic.UI
{
    public interface IUpdateBehaviour : IBehaviour
    {
        void Update(IView view, float deltaTime);
    }
}