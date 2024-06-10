using System;
using Atomic.Objects;
using UnityEngine.Scripting.APIUpdating;

namespace Atomic.Extensions
{
    [MovedFrom(true, "Atomic.Objects", "Atomic.Objects")] 
    public interface IActionBuilder
    {
        Action Build(IObject obj);
    }

    public interface IActionBuilder<in T>
    {
        Action<T> Build(IObject obj);
    }
    
    public interface IActionBuilder<in T1, in T2>
    {
        Action<T1, T2> Build(IObject obj);
    }
    
    public interface IActionBuilder<in T1, in T2, in T3>
    {
        Action<T1, T2, T3> Build(IObject obj);
    }
}