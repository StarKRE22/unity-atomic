namespace Atomic
{
    public interface IAtomicUpdate : IAtomicLogic
    {
        void OnUpdate(float deltaTime);
    }
}