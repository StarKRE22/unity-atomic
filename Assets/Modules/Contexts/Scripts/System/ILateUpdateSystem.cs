namespace Modules.Contexts
{
    public interface ILateUpdateSystem : ISystem
    {
        void LateUpdate(float deltaTime);
    }
}