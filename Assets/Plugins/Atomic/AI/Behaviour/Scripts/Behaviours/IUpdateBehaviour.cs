namespace Modules.AI
{
    public interface IUpdateBehaviour : IBehaviour
    {
        void OnUpdate(IBlackboard blackboard, float deltaTime);
    }
}