using System;

namespace Atomic.Elements
{
    public interface IAtomicExpression<T> : IAtomicFunction<T>
    {
        void Append(Func<T> member);
        void Remove(Func<T> member);
        bool Contains(Func<T> member);
    }

    public interface IAtomicExpression<T, R> : IAtomicFunction<T, R>
    {
        void Append(Func<T, R> member);
        void Remove(Func<T, R> member);
        bool Contains(Func<T, R> member);
    }
    
    public interface IAtomicExpression<T1, T2, R> : IAtomicFunction<T1, T2, R>
    {
        void Append(Func<T1, T2, R> member);
        void Remove(Func<T1, T2, R> member);
        bool Contains(Func<T1, T2, R> member);
    }
}