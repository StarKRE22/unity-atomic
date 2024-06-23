using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    public static class ElementExtensions
    {
        public static void Invoke(this IEnumerable<IAtomicAction> actions)
        {
            if (actions != null)
            {
                foreach (IAtomicAction action in actions)
                {
                    action.Invoke();
                }    
            }
        } 
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AppendAll<T>(this IAtomicExpression<T> it, params Func<T>[] members)
        {
            for (int i = 0, count = members.Length; i < count; i++)
            {
                it.Append(members[i]);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Append<T>(this IAtomicExpression<T> it, IAtomicFunction<T> member)
        {
            it.Append(member.Invoke);
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
        public static AtomicFunction<R> AsFunction<R>(this Func<R> func)
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
        public static void Subscribe<T1, T2, T3>(this IAtomicObservable<T1, T2, T3> it,
            IAtomicAction<T1, T2, T3> action)
        {
            it.Subscribe(action.Invoke);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unsubscribe<T1, T2, T3>(this IAtomicObservable<T1, T2, T3> it,
            IAtomicAction<T1, T2, T3> action)
        {
            it.Unsubscribe(action.Invoke);
        }
    }
}