namespace Atomic.Contexts
{
    public interface ILateUpdateSystem : ISystem
    {
        void LateUpdate(IContext context, float deltaTime);
    }
}