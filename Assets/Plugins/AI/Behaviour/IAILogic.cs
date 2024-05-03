namespace AIModule
{
    public interface IAILogic
    {
    }

    public interface IAIStartable : IAILogic
    {
        void OnStart(IBlackboard blackboard);
    }

    public interface IAIStoppable : IAILogic
    {
        void OnStop(IBlackboard blackboard);
    }

    public interface IAIUpdatable : IAILogic
    {
        void OnUpdate(IBlackboard blackboard, float deltaTime);
    }

    public interface IAIGizmos
    {
        void OnGizmos(IBlackboard blackboard);
    }

    public interface IAIValidate
    {
        void OnValidate();
    }
}