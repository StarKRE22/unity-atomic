using UnityEngine.Scripting.APIUpdating;

namespace Modules.AI
{
    [MovedFrom(true, null, null, "IAIAction")] 
    public interface IBlackboardAction
    {
        void Invoke(IBlackboard blackboard);
    }

    public interface IBlackboardAction<in T>
    {
        void Invoke(IBlackboard blackboard, T arg);
    }
    
    public interface IBlackboardAction<in T1, in T2>
    {
        void Invoke(IBlackboard blackboard, T1 arg1, T2 arg2);
    }

    public interface IBlackboardActionRef<T>
    {
        void Invoke(IBlackboard blackboard, ref T arg);
    }
}