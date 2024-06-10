using System;
using Atomic.Objects;
using UnityEngine.Scripting.APIUpdating;

namespace Atomic.Extensions
{
    [MovedFrom(true, "Atomic.Objects", "Atomic.Objects")]
    public interface IConditionBuilder
    {
        Func<bool> Build(IObject obj);
    }
    
    public interface IConditionBuilder<in T>
    {
        Func<T, bool> Build(IObject obj);
    }
    
    public interface IConditionBuilder<in T1, in T2>
    {
        Func<T1, T2, bool> Build(IObject obj);
    }
    
    public interface IConditionBuilder<in T1, in T2, in T3>
    {
        Func<T1, T2, T3, bool> Build(IObject obj);
    }
}
