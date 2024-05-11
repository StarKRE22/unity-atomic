namespace Atomic.Elements
{
    public interface IAtomicExpression<T> : IAtomicFunction<T>
    {
        void Append(IAtomicValue<T> member);
        void Remove(IAtomicValue<T> member);
    }

    public interface IAtomicExpression<T, R> : IAtomicFunction<T, R>
    {
        void Append(IAtomicFunction<T, R> member);
        void Remove(IAtomicFunction<T, R> member);
    }
}