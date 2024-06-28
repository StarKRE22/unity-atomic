namespace Atomic.Contexts
{
    public interface IUpdateSystem : ISystem
    {
        void Update(IContext context, float deltaTime);
    }
}