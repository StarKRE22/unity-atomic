namespace AIModule
{
    public abstract class AIMechanics : AILogic, IAIUpdatable
    {
        public abstract void OnUpdate(IBlackboard blackboard, float deltaTime);
    }
}