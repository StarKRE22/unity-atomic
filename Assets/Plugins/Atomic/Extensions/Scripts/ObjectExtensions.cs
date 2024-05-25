using System;
using System.Runtime.CompilerServices;
using Atomic.Elements;
using Atomic.Objects;
using JetBrains.Annotations;

namespace Atomic
{
    public static class ObjectExtensions
    {
        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicValue<T> GetAtomicValue<T>(this IAtomicObject it, int id)
        {
            return it.GetValue<IAtomicValue<T>>(id);
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicVariable<T> GetAtomicVariable<T>(this IAtomicObject it, int id)
        {
            return it.GetValue<IAtomicVariable<T>>(id);
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicFunction<T> GetAtomicFunction<T>(this IAtomicObject it, int id)
        {
            return it.GetValue<IAtomicFunction<T>>(id);
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicFunction<T1, T2> GetAtomicFunction<T1, T2>(this IAtomicObject it, int id)
        {
            return it.GetValue<IAtomicFunction<T1, T2>>(id);
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicFunction<T1, T2, T3> GetAtomicFunction<T1, T2, T3>(this IAtomicObject it, int id)
        {
            return it.GetValue<IAtomicFunction<T1, T2, T3>>(id);
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicAction GetAtomicAction(this IAtomicObject it, int id)
        {
            return it.GetValue<IAtomicAction>(id);
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicAction<T> GetAtomicAction<T>(this IAtomicObject it, int id)
        {
            return it.GetValue<IAtomicAction<T>>(id);
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicAction<T1, T2> GetAtomicAction<T1, T2>(this IAtomicObject it, int id)
        {
            return it.GetValue<IAtomicAction<T1, T2>>(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeAtomicAction(this IAtomicObject it, int id)
        {
            it.GetAtomicAction(id)?.Invoke();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeAtomicAction<T>(this IAtomicObject it, int id, T args)
        {
            it.GetAtomicAction<T>(id)?.Invoke(args);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T InvokeAtomicFunction<T>(this IAtomicObject it, int id)
        {
            IAtomicFunction<T> function = it.GetAtomicFunction<T>(id);
            if (function != null) return function.Invoke();
            return default;
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicSetter<T> GetAtomicSetter<T>(this IAtomicObject it, int id)
        {
            return it.GetValue<IAtomicSetter<T>>(id);
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicExpression<T> GetAtomicExpression<T>(this IAtomicObject it, int id)
        {
            return it.GetValue<IAtomicExpression<T>>(id);
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicObservable GetAtomicObservable(this IAtomicObject it, int id)
        {
            return it.GetValue<IAtomicObservable>(id);
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicObservable<T> GetAtomicObservable<T>(this IAtomicObject it, int id)
        {
            return it.GetValue<IAtomicObservable<T>>(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SubscribeOnAtomicEvent(this IAtomicObject it, int id, Action listener)
        {
            if (it.TryGetValue(id, out IAtomicObservable observable))
            {
                observable.Subscribe(listener);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SubscribeOnAtomicEvent(this IAtomicObject it, int id, IAtomicAction listener)
        {
            if (it.TryGetValue(id, out IAtomicObservable observable))
            {
                observable.Subscribe(listener.Invoke);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool UnsubscribeFromAtomicEvent(this IAtomicObject it, int id, Action listener)
        {
            if (it.TryGetValue(id, out IAtomicObservable observable))
            {
                observable.Unsubscribe(listener);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool UnsubscribeFromAtomicEvent(this IAtomicObject it, int id, IAtomicAction listener)
        {
            if (it.TryGetValue(id, out IAtomicObservable observable))
            {
                observable.Unsubscribe(listener.Invoke);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SubscribeOnAtomicEvent<T>(this IAtomicObject it, int id, Action<T> listener)
        {
            if (it.TryGetValue(id, out IAtomicObservable<T> observable))
            {
                observable.Subscribe(listener);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SubscribeOnAtomicEvent<T>(this IAtomicObject it, int id, IAtomicAction<T> listener)
        {
            if (it.TryGetValue(id, out IAtomicObservable<T> observable))
            {
                observable.Subscribe(listener.Invoke);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool UnsubscribeFromAtomicEvent<T>(this IAtomicObject it, int id, Action<T> listener)
        {
            if (it.TryGetValue(id, out IAtomicObservable<T> observable))
            {
                observable.Unsubscribe(listener);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool UnsubscribeFromAtomicEvent<T>(this IAtomicObject it, int id, IAtomicAction<T> listener)
        {
            if (it.TryGetValue(id, out IAtomicObservable<T> observable))
            {
                observable.Unsubscribe(listener.Invoke);
                return true;
            }

            return false;
        }
    }
}