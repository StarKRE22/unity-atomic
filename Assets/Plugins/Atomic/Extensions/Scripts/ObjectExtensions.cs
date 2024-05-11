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
        public static IAtomicValue<T> GetValue<T>(this IAtomicObject it, int id)
        {
            return it.GetReference<IAtomicValue<T>>(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<T>(this IAtomicObject it, int id, out IAtomicValue<T> result)
        {
            return it.TryGetReference(id, out result) && result != null;
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicVariable<T> GetVariable<T>(this IAtomicObject it, int id)
        {
            return it.GetReference<IAtomicVariable<T>>(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetVariable<T>(this IAtomicObject it, int id, out IAtomicVariable<T> result)
        {
            return it.TryGetReference(id, out result) && result != null;
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicFunction<T> GetFunction<T>(this IAtomicObject it, int id)
        {
            return it.GetReference<IAtomicFunction<T>>(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetFunction<T>(this IAtomicObject it, int id, out IAtomicFunction<T> result)
        {
            return it.TryGetReference(id, out result) && result != null;
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicFunction<T1, T2> GetFunction<T1, T2>(this IAtomicObject it, int id)
        {
            return it.GetReference<IAtomicFunction<T1, T2>>(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetFunction<T1, T2>(
            this IAtomicObject it,
            int id,
            out IAtomicFunction<T1, T2> result
        ) => it.TryGetReference(id, out result) && result != null;

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicFunction<T1, T2, T3> GetFunction<T1, T2, T3>(this IAtomicObject it, int id)
        {
            return it.GetReference<IAtomicFunction<T1, T2, T3>>(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetFunction<T1, T2, T3>(
            this IAtomicObject it,
            int id,
            out IAtomicFunction<T1, T2, T3> result
        ) => it.TryGetReference(id, out result) && result != null;

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicAction GetAction(this IAtomicObject it, int id)
        {
            return it.GetReference<IAtomicAction>(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAction(this IAtomicObject it, int id, out IAtomicAction result)
        {
            return it.TryGetReference(id, out result) && result != null;
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicAction<T> GetAction<T>(this IAtomicObject it, int id)
        {
            return it.GetReference<IAtomicAction<T>>(id);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAction<T>(this IAtomicObject it, int id, out IAtomicAction<T> result)
        {
            return it.TryGetReference(id, out result) && result != null;
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicAction<T1, T2> GetAction<T1, T2>(this IAtomicObject it, int id)
        {
            return it.GetReference<IAtomicAction<T1, T2>>(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAction<T1, T2>(this IAtomicObject it, int id, out IAtomicAction<T1, T2> result)
        {
            return it.TryGetReference(id, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeAction(this IAtomicObject it, int id)
        {
            it.GetAction(id)?.Invoke();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeAction<T>(this IAtomicObject it, int id, T args)
        {
            it.GetAction<T>(id)?.Invoke(args);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T InvokeFunction<T>(this IAtomicObject it, int id)
        {
            IAtomicFunction<T> function = it.GetFunction<T>(id);
            if (function != null) return function.Invoke();
            return default;
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicSetter<T> GetSetter<T>(this IAtomicObject it, int id)
        {
            return it.GetReference<IAtomicSetter<T>>(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetVariable<T>(this IAtomicObject it, int id, T value)
        {
            if (it.TryGetReference(id, out IAtomicSetter<T> setter)) setter.Value = value;
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicExpression<T> GetExpression<T>(this IAtomicObject it, int id)
        {
            return it.GetReference<IAtomicExpression<T>>(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetExpression<T>(this IAtomicObject it, int id, out IAtomicExpression<T> result)
        {
            return it.TryGetReference(id, out result) && result != null;
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicObservable GetObservable(this IAtomicObject it, int id)
        {
            return it.GetReference<IAtomicObservable>(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetObservable(this IAtomicObject it, int id, out IAtomicObservable result)
        {
            return it.TryGetReference(id, out result) && result != null;
        }

        [CanBeNull]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicObservable<T> GetObservable<T>(this IAtomicObject it, int id)
        {
            return it.GetReference<IAtomicObservable<T>>(id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetObservable<T>(this IAtomicObject it, int id, out IAtomicObservable<T> result)
        {
            return it.TryGetReference(id, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SubscribeOnEvent(this IAtomicObject it, int id, Action listener)
        {
            if (it.TryGetObservable(id, out IAtomicObservable observable))
            {
                observable.Subscribe(listener);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SubscribeOnEvent(this IAtomicObject it, int id, IAtomicAction listener)
        {
            if (it.TryGetObservable(id, out IAtomicObservable observable))
            {
                observable.Subscribe(listener.Invoke);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool UnsubscribeFromEvent(this IAtomicObject it, int id, Action listener)
        {
            if (it.TryGetObservable(id, out IAtomicObservable observable))
            {
                observable.Unsubscribe(listener);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool UnsubscribeFromEvent(this IAtomicObject it, int id, IAtomicAction listener)
        {
            if (it.TryGetObservable(id, out IAtomicObservable observable))
            {
                observable.Unsubscribe(listener.Invoke);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SubscribeOnEvent<T>(this IAtomicObject it, int id, Action<T> listener)
        {
            if (it.TryGetObservable(id, out IAtomicObservable<T> observable))
            {
                observable.Subscribe(listener);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SubscribeOnEvent<T>(this IAtomicObject it, int id, IAtomicAction<T> listener)
        {
            if (it.TryGetObservable(id, out IAtomicObservable<T> observable))
            {
                observable.Subscribe(listener.Invoke);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool UnsubscribeFromEvent<T>(this IAtomicObject it, int id, Action<T> listener)
        {
            if (it.TryGetObservable(id, out IAtomicObservable<T> observable))
            {
                observable.Unsubscribe(listener);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool UnsubscribeFromEvent<T>(this IAtomicObject it, int id, IAtomicAction<T> listener)
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