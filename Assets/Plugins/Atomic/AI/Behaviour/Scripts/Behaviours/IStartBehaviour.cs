namespace Modules.AI
{
    public interface IStartBehaviour : IBehaviour
    {
        void OnStart(IBlackboard blackboard);
    }
}