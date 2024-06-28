namespace Atomic.Contexts
{
    public interface IFixedUpdateSystem
    {
        void FixedUpdate(IContext context, float deltaTime);
    }
}