namespace Atomic.Elements
{
    public interface IAtomicEvent : IAtomicObservable, IAtomicAction
    {
    }

    public interface IAtomicEvent<T> : IAtomicObservable<T>, IAtomicAction<T>
    {
    }

    public interface IAtomicEvent<T1, T2> : IAtomicObservable<T1, T2>, IAtomicAction<T1, T2>
    {
    }

    public interface IAtomicEvent<T1, T2, T3> : IAtomicObservable<T1, T2, T3>, IAtomicAction<T1, T2, T3>
    {
    }
}