namespace Atomic.Contexts
{
    public interface ILateUpdateSystem : ISystem
    {
        void LateUpdate(float deltaTime);
    }
}