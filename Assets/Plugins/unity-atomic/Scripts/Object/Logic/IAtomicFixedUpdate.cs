namespace Atomic
{
    public interface IAtomicFixedUpdate : IAtomicLogic
    {
        void OnFixedUpdate(float deltaTime);
    }
}