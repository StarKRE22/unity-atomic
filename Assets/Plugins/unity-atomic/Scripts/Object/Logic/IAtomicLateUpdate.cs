namespace Atomic
{
    public interface IAtomicLateUpdate : IAtomicLogic
    {
        void OnLateUpdate(float deltaTime);
    }
}