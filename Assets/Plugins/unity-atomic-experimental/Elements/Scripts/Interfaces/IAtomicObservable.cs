namespace Atomic.Elements
{
    public interface IAtomicObservable
    {
        void Subscribe(System.Action action);
        void Unsubscribe(System.Action action);
    }

    public interface IAtomicObservable<out T>
    {
        void Subscribe(System.Action<T> action);
        void Unsubscribe(System.Action<T> action);
    }

    public interface IAtomicObservable<out T1, out T2>
    {
        void Subscribe(System.Action<T1, T2> action);
        void Unsubscribe(System.Action<T1, T2> action);
    }

    public interface IAtomicObservable<out T1, out T2, out T3>
    {
        void Subscribe(System.Action<T1, T2, T3> action);
        void Unsubscribe(System.Action<T1, T2, T3> action);
    }
}