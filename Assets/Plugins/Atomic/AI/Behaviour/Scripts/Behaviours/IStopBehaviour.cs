namespace Modules.AI
{
    public interface IStopBehaviour : IBehaviour
    {
        void OnStop(IBlackboard blackboard);
    }
}