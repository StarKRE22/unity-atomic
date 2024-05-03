using System;
using System.Runtime.CompilerServices;

namespace Atomic
{
    public static class EntityExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicValue<T> GetValue<T>(this IAtomicEntity it, int index)
        {
            return it.Get<IAtomicValue<T>>(index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<T>(this IAtomicEntity it, int index, out IAtomicValue<T> result)
        {
            return it.TryGet(index, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicVariable<T> GetVariable<T>(this IAtomicEntity it, int index)
        {
            return it.Get<IAtomicVariable<T>>(index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetVariable<T>(this IAtomicEntity it, int index, out IAtomicVariable<T> result)
        {
            return it.TryGet(index, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicFunction<T> GetFunction<T>(this IAtomicEntity it, int index)
        {
            return it.Get<IAtomicFunction<T>>(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetFunction<T>(this IAtomicEntity it, int index, out IAtomicFunction<T> result)
        {
            return it.TryGet(index, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicFunction<T1, T2> GetFunction<T1, T2>(this IAtomicEntity it, int index)
        {
            return it.Get<IAtomicFunction<T1, T2>>(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetFunction<T1, T2>(
            this IAtomicEntity it,
            int index,
            out IAtomicFunction<T1, T2> result
        ) => it.TryGet(index, out result) && result != null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicFunction<T1, T2, T3> GetFunction<T1, T2, T3>(this IAtomicEntity it, int index)
        {
            return it.Get<IAtomicFunction<T1, T2, T3>>(index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetFunction<T1, T2, T3>(
            this IAtomicEntity it,
            int index,
            out IAtomicFunction<T1, T2, T3> result
        ) => it.TryGet(index, out result) && result != null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicAction GetAction(this IAtomicEntity it, int index)
        {
            return it.Get<IAtomicAction>(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAction(this IAtomicEntity it, int index, out IAtomicAction result)
        {
            return it.TryGet(index, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicAction<T> GetAction<T>(this IAtomicEntity it, int index)
        {
            return it.Get<IAtomicAction<T>>(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAction<T>(this IAtomicEntity it, int index, out IAtomicAction<T> result)
        {
            return it.TryGet(index, out result) && result != null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicAction<T1, T2> GetAction<T1, T2>(this IAtomicEntity it, int index)
        {
            return it.Get<IAtomicAction<T1, T2>>(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAction<T1, T2>(this IAtomicEntity it, int index, out IAtomicAction<T1, T2> result)
        {
            return it.TryGet(index, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeAction(this IAtomicEntity it, int index)
        {
            it.GetAction(index)?.Invoke();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeAction<T>(this IAtomicEntity it, int index, T args)
        {
            it.GetAction<T>(index)?.Invoke(args);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T InvokeFunction<T>(this IAtomicEntity it, int index)
        {
            IAtomicFunction<T> function = it.GetFunction<T>(index);
            if (function != null) return function.Invoke();
            return default;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicSetter<T> GetSetter<T>(this IAtomicEntity it, int index)
        {
            return it.Get<IAtomicSetter<T>>(index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetVariable<T>(this IAtomicEntity it, int index, T value)
        {
            if (it.TryGet(index, out IAtomicSetter<T> setter)) setter.Value = value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicExpression<T> GetExpression<T>(this IAtomicEntity it, int index)
        {
            return it.Get<IAtomicExpression<T>>(index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetExpression<T>(this IAtomicEntity it, int index, out IAtomicExpression<T> result)
        {
            return it.TryGet(index, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicObservable GetObservable(this IAtomicEntity it, int index)
        {
            return it.Get<IAtomicObservable>(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetObservable(this IAtomicEntity it, int index, out IAtomicObservable result)
        {
            return it.TryGet(index, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicObservable<T> GetObservable<T>(this IAtomicEntity it, int index)
        {
            return it.Get<IAtomicObservable<T>>(index);
        }
     
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetObservable<T>(this IAtomicEntity it, int index, out IAtomicObservable<T> result)
        {
            return it.TryGet(index, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SubscribeOnEvent(this IAtomicEntity it, int index, Action listener)
        {
            if (it.TryGetObservable(index, out IAtomicObservable observable))
            {
                observable.Subscribe(listener);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SubscribeOnEvent(this IAtomicEntity it, int index, IAtomicAction listener)
        {
            if (it.TryGetObservable(index, out IAtomicObservable observable))
            {
                observable.Subscribe(listener.Invoke);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool UnsubscribeFromEvent(this IAtomicEntity it, int index, Action listener)
        {
            if (it.TryGetObservable(index, out IAtomicObservable observable))
            {
                observable.Unsubscribe(listener);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool UnsubscribeFromEvent(this IAtomicEntity it, int index, IAtomicAction listener)
        {
            if (it.TryGetObservable(index, out IAtomicObservable observable))
            {
                observable.Unsubscribe(listener.Invoke);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SubscribeOnEvent<T>(this IAtomicEntity it, int index, Action<T> listener)
        {
            if (it.TryGetObservable(index, out IAtomicObservable<T> observable))
            {
                observable.Subscribe(listener);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SubscribeOnEvent<T>(this IAtomicEntity it, int index, IAtomicAction<T> listener)
        {
            if (it.TryGetObservable(index, out IAtomicObservable<T> observable))
            {
                observable.Subscribe(listener.Invoke);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool UnsubscribeFromEvent<T>(this IAtomicEntity it, int index, Action<T> listener)
        {
            if (it.TryGetObservable(index, out IAtomicObservable<T> observable))
            {
                observable.Unsubscribe(listener);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool UnsubscribeFromEvent<T>(this IAtomicEntity it, int index, IAtomicAction<T> listener)
        {
            if (it.TryGetObservable(index, out IAtomicObservable<T> observable))
            {
                observable.Unsubscribe(listener.Invoke);
                return true;
            }

            return false;
        }
    }
}

