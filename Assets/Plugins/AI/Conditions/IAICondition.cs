namespace AIModule
{
    public interface IAICondition
    {
        bool Check(IBlackboard blackboard);
    }
}