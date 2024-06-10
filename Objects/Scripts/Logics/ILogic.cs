namespace Atomic.Objects
{
    public interface ILogic
    {
    }

    public interface IConstructable : ILogic
    {
        void Construct(IObject obj);
    }
    
    public interface IEnable : ILogic
    {
        void Enable();
    }

    public interface IDisable : ILogic
    {
        void Disable();
    }

    public interface IFixedUpdate : ILogic
    {
        void OnFixedUpdate(float deltaTime);
    }

    public interface ILateUpdate : ILogic
    {
        void OnLateUpdate(float deltaTime);
    }

    public interface IUpdate : ILogic
    {
        void OnUpdate(float deltaTime);
    }
}