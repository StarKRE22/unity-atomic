using System;
using Sirenix.OdinInspector;

namespace Atomic.Elements
{
    ///Combines AtomicProperty & AtomicObservable
    
    public sealed class AtomicPropertyObservable<T> : IAtomicVariable<T>, IAtomicObservable<T>
    {
        private Func<T> getter;
        private Action<T> setter;

        private Action<Action<T>> subscribe;
        private Action<Action<T>> unsubscribe;
        
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        public T Value
        {
            get { return this.getter != null ? this.getter.Invoke() : default; }
            set { this.setter?.Invoke(value); }
        }
        
        public void Subscribe(Action<T> action)
        {
            this.subscribe.Invoke(action);
        }

        public void Unsubscribe(Action<T> action)
        {
            this.unsubscribe.Invoke(action);
        }

        public AtomicPropertyObservable()
        {
        }

        public AtomicPropertyObservable(
            Func<T> getter,
            Action<T> setter,
            Action<Action<T>> subscribe,
            Action<Action<T>> unsubscribe
        )
        {
            this.getter = getter;
            this.setter = setter;
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public void Compose(
            Func<T> getter,
            Action<T> setter,
            Action<Action<T>> subscribe,
            Action<Action<T>> unsubscribe
        )
        {
            this.getter = getter;
            this.setter = setter;
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }
    }
}
