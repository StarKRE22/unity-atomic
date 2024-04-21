namespace Atomic.Objects
{
    public interface IAtomicLogic
    {
    }

    public interface IAtomicEnable : IAtomicLogic
    {
        void Enable();
    }

    public interface IAtomicDisable : IAtomicLogic
    {
        void Disable();
    }

    public interface IAtomicUpdate : IAtomicLogic
    {
        void OnUpdate(float deltaTime);
    }

    public interface IAtomicFixedUpdate : IAtomicLogic
    {
        void OnFixedUpdate(float deltaTime);
    }

    public interface IAtomicLateUpdate : IAtomicLogic
    {
        void OnLateUpdate(float deltaTime);
    }
}