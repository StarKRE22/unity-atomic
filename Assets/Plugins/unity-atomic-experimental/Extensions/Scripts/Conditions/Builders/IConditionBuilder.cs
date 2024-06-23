namespace Atomic.Extensions
{
    public interface IConditionBuilder : IFunctionBuilder<bool>
    {
    }
    
    public interface IConditionBuilder<in T> : IFunctionBuilder<T, bool>
    {
    }
    
    public interface IConditionBuilder<in T1, in T2> : IFunctionBuilder<T1, T2, bool>
    {
    }
    
    public interface IConditionBuilder<in T1, in T2, in T3> : IFunctionBuilder<T1, T2, T3, bool>
    {
    }
}
