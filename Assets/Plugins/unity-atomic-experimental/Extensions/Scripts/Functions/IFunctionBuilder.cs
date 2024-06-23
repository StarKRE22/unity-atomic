using System;
using Atomic.Objects;

namespace Atomic.Extensions
{
    public interface IFunctionBuilder<out R>
    {
        Func<R> Build(IObject obj);
    }

    public interface IFunctionBuilder<in T, out R>
    {
        Func<T, R> Build(IObject obj);
    }

    public interface IFunctionBuilder<in T1, in T2, out R>
    {
        Func<T1, T2, R> Build(IObject obj);
    }

    public interface IFunctionBuilder<in T1, in T2, in T3, out R>
    {
        Func<T1, T2, T3, R> Build(IObject obj);
    }
}