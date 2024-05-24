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
        public static IAtomicValue<T> GetValue<T>(this IObject it, int id)
        {
            return it.GetValue<IAtomicValue<T>>(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<T>(this IObject it, int id, out IAtomicValue<T> result)
        {
            return it.TryGet(id, out result) && result != null;
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicVariable<T> GetVariable<T>(this IObject it, int id)
        {
            return it.GetValue<IAtomicVariable<T>>(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetVariable<T>(this IObject it, int id, out IAtomicVariable<T> result)
        {
            return it.TryGet(id, out result) && result != null;
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicFunction<T> GetFunction<T>(this IObject it, int id)
        {
            return it.GetValue<IAtomicFunction<T>>(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetFunction<T>(this IObject it, int id, out IAtomicFunction<T> result)
        {
            return it.TryGet(id, out result) && result != null;
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicFunction<T1, T2> GetFunction<T1, T2>(this IObject it, int id)
        {
            return it.GetValue<IAtomicFunction<T1, T2>>(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetFunction<T1, T2>(
            this IObject it,
            int id,
            out IAtomicFunction<T1, T2> result
        ) => it.TryGet(id, out result) && result != null;

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicFunction<T1, T2, T3> GetFunction<T1, T2, T3>(this IObject it, int id)
        {
            return it.GetValue<IAtomicFunction<T1, T2, T3>>(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetFunction<T1, T2, T3>(
            this IObject it,
            int id,
            out IAtomicFunction<T1, T2, T3> result
        ) => it.TryGet(id, out result) && result != null;

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicAction GetAction(this IObject it, int id)
        {
            return it.GetValue<IAtomicAction>(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAction(this IObject it, int id, out IAtomicAction result)
        {
            return it.TryGet(id, out result) && result != null;
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicAction<T> GetAction<T>(this IObject it, int id)
        {
            return it.GetValue<IAtomicAction<T>>(id);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAction<T>(this IObject it, int id, out IAtomicAction<T> result)
        {
            return it.TryGet(id, out result) && result != null;
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicAction<T1, T2> GetAction<T1, T2>(this IObject it, int id)
        {
            return it.GetValue<IAtomicAction<T1, T2>>(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAction<T1, T2>(this IObject it, int id, out IAtomicAction<T1, T2> result)
        {
            return it.TryGet(id, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeAction(this IObject it, int id)
        {
            it.GetAction(id)?.Invoke();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeAction<T>(this IObject it, int id, T args)
        {
            it.GetAction<T>(id)?.Invoke(args);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T InvokeFunction<T>(this IObject it, int id)
        {
            IAtomicFunction<T> function = it.GetFunction<T>(id);
            if (function != null) return function.Invoke();
            return default;
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicSetter<T> GetSetter<T>(this IObject it, int id)
        {
            return it.GetValue<IAtomicSetter<T>>(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetVariable<T>(this IObject it, int id, T value)
        {
            if (it.TryGet(id, out IAtomicSetter<T> setter)) setter.Value = value;
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicExpression<T> GetExpression<T>(this IObject it, int id)
        {
            return it.GetValue<IAtomicExpression<T>>(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetExpression<T>(this IObject it, int id, out IAtomicExpression<T> result)
        {
            return it.TryGet(id, out result) && result != null;
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicObservable GetObservable(this IObject it, int id)
        {
            return it.GetValue<IAtomicObservable>(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetObservable(this IObject it, int id, out IAtomicObservable result)
        {
            return it.TryGet(id, out result) && result != null;
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicObservable<T> GetObservable<T>(this IObject it, int id)
        {
            return it.GetValue<IAtomicObservable<T>>(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetObservable<T>(this IObject it, int id, out IAtomicObservable<T> result)
        {
            return it.TryGet(id, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SubscribeOnEvent(this IObject it, int id, Action listener)
        {
            if (it.TryGetObservable(id, out IAtomicObservable observable))
            {
                observable.Subscribe(listener);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SubscribeOnEvent(this IObject it, int id, IAtomicAction listener)
        {
            if (it.TryGetObservable(id, out IAtomicObservable observable))
            {
                observable.Subscribe(listener.Invoke);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool UnsubscribeFromEvent(this IObject it, int id, Action listener)
        {
            if (it.TryGetObservable(id, out IAtomicObservable observable))
            {
                observable.Unsubscribe(listener);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool UnsubscribeFromEvent(this IObject it, int id, IAtomicAction listener)
        {
            if (it.TryGetObservable(id, out IAtomicObservable observable))
            {
                observable.Unsubscribe(listener.Invoke);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SubscribeOnEvent<T>(this IObject it, int id, Action<T> listener)
        {
            if (it.TryGetObservable(id, out IAtomicObservable<T> observable))
            {
                observable.Subscribe(listener);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SubscribeOnEvent<T>(this IObject it, int id, IAtomicAction<T> listener)
        {
            if (it.TryGetObservable(id, out IAtomicObservable<T> observable))
            {
                observable.Subscribe(listener.Invoke);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool UnsubscribeFromEvent<T>(this IObject it, int id, Action<T> listener)
        {
            if (it.TryGetObservable(id, out IAtomicObservable<T> observable))
            {
                observable.Unsubscribe(listener);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool UnsubscribeFromEvent<T>(this IObject it, int id, IAtomicAction<T> listener)
        {
            if (it.TryGetObservable(id, out IAtomicObservable<T> observable))
            {
                observable.Unsubscribe(listener.Invoke);
                return true;
            }

            return false;
        }
    }
}