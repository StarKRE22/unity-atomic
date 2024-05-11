using System;

namespace Atomic.Elements
{
    /// Provides observable interface to a specified source.
    
    [Serializable]
    public class AtomicObservable : IAtomicObservable
    {
        private Action<Action> subscribe;
        private Action<Action> unsubscribe;

        public AtomicObservable()
        {
        }

        public AtomicObservable(Action<Action> subscribe, Action<Action> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public void Compose(Action<Action> subscribe, Action<Action> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public void Subscribe(Action action)
        {
            this.subscribe?.Invoke(action);
        }

        public void Unsubscribe(Action action)
        {
            this.unsubscribe?.Invoke(action);
        }
    }

    [Serializable]
    public sealed class AtomicObservable<T> : IAtomicObservable<T>
    {
        private Action<Action<T>> subscribe;
        private Action<Action<T>> unsubscribe;

        public AtomicObservable()
        {
        }

        public AtomicObservable(Action<Action<T>> subscribe, Action<Action<T>> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public void Compose(Action<Action<T>> subscribe, Action<Action<T>> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public void Subscribe(Action<T> action)
        {
            this.subscribe.Invoke(action);
        }

        public void Unsubscribe(Action<T> action)
        {
            this.unsubscribe.Invoke(action);
        }
    }
    
    [Serializable]
    public sealed class AtomicObservable<T1, T2> : IAtomicObservable<T1, T2>
    {
        private Action<Action<T1, T2>> subscribe;
        private Action<Action<T1, T2>> unsubscribe;

        public AtomicObservable()
        {
        }

        public AtomicObservable(Action<Action<T1, T2>> subscribe, Action<Action<T1, T2>> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public void Compose(Action<Action<T1, T2>> subscribe, Action<Action<T1, T2>> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public void Subscribe(Action<T1, T2> action)
        {
            this.subscribe.Invoke(action);
        }

        public void Unsubscribe(Action<T1, T2> action)
        {
            this.unsubscribe.Invoke(action);
        }
    }
    
    [Serializable]
    public sealed class AtomicObservable<T1, T2, T3> : IAtomicObservable<T1, T2, T3>
    {
        private Action<Action<T1, T2, T3>> subscribe;
        private Action<Action<T1, T2, T3>> unsubscribe;

        public AtomicObservable()
        {
        }

        public AtomicObservable(Action<Action<T1, T2, T3>> subscribe, Action<Action<T1, T2, T3>> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public void Compose(Action<Action<T1, T2, T3>> subscribe, Action<Action<T1, T2, T3>> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public void Subscribe(Action<T1, T2, T3> action)
        {
            this.subscribe.Invoke(action);
        }

        public void Unsubscribe(Action<T1, T2, T3> action)
        {
            this.unsubscribe.Invoke(action);
        }
    }
}
