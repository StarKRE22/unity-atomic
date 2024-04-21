using System;
using System.Runtime.CompilerServices;
using Atomic.Elements;

namespace Atomic.Extensions
{
    public static class ElementExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Let<T>(this T it, Action<T> let)
        {
            let.Invoke(it);
            return it;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AtomicValue<T> AsValue<T>(this T it)
        {
            return new AtomicValue<T>(it);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AtomicVariable<T> AsVariable<T>(this T it)
        {
            return new AtomicVariable<T>(it);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AtomicFunction<R> AsFunction<R>(Func<R> func)
        {
            return new AtomicFunction<R>(func);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AtomicFunction<R> AsFunction<T, R>(this T it, Func<T, R> func)
        {
            return new AtomicFunction<R>(() => func.Invoke(it));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AtomicFunction<bool> AsNot(this IAtomicValue<bool> it)
        {
            return new AtomicFunction<bool>(() => !it.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AtomicProperty<R> AsProperty<T, R>(this T it, Func<T, R> getter, Action<T, R> setter)
        {
            return new AtomicProperty<R>(() => getter.Invoke(it), value => setter.Invoke(it, value));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subscribe(this IAtomicObservable it, IAtomicAction action)
        {
            it.Subscribe(action.Invoke);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unsubscribe(this IAtomicObservable it, IAtomicAction action)
        {
            it.Unsubscribe(action.Invoke);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subscribe<T>(this IAtomicObservable<T> it, IAtomicAction<T> action)
        {
            it.Subscribe(action.Invoke);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unsubscribe<T>(this IAtomicObservable<T> it, IAtomicAction<T> action)
        {
            it.Unsubscribe(action.Invoke);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subscribe<T1, T2>(this IAtomicObservable<T1, T2> it, IAtomicAction<T1, T2> action)
        {
            it.Subscribe(action.Invoke);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unsubscribe<T1, T2>(this IAtomicObservable<T1, T2> it, IAtomicAction<T1, T2> action)
        {
            it.Unsubscribe(action.Invoke);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subscribe<T1, T2, T3>(this IAtomicObservable<T1, T2, T3> it, IAtomicAction<T1, T2, T3> action)
        {
            it.Subscribe(action.Invoke);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unsubscribe<T1, T2, T3>(this IAtomicObservable<T1, T2, T3> it, IAtomicAction<T1, T2, T3> action)
        {
            it.Unsubscribe(action.Invoke);
        }
    }
}