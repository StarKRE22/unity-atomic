using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Atomic.Elements;
using Atomic.Objects;

namespace Atomic.Extensions
{
    public static class EntityExtensions
    {
        private static readonly List<IAtomicLogic> cache = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicValue<T> GetValue<T>(this IAtomicEntity it, string name)
        {
            return it.Get<IAtomicValue<T>>(name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<T>(this IAtomicEntity it, string name, out IAtomicValue<T> result)
        {
            return it.TryGet(name, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicVariable<T> GetVariable<T>(this IAtomicEntity it, string name)
        {
            return it.Get<IAtomicVariable<T>>(name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetVariable<T>(this IAtomicEntity it, string name, out IAtomicVariable<T> result)
        {
            return it.TryGet(name, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicFunction<T> GetFunction<T>(this IAtomicEntity it, string name)
        {
            return it.Get<IAtomicFunction<T>>(name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetFunction<T>(this IAtomicEntity it, string name, out IAtomicFunction<T> result)
        {
            return it.TryGet(name, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicFunction<T1, T2> GetFunction<T1, T2>(this IAtomicEntity it, string name)
        {
            return it.Get<IAtomicFunction<T1, T2>>(name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetFunction<T1, T2>(
            this IAtomicEntity it,
            string name,
            out IAtomicFunction<T1, T2> result
        )
        {
            return it.TryGet(name, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicFunction<T1, T2, T3> GetFunction<T1, T2, T3>(this IAtomicEntity it, string name)
        {
            return it.Get<IAtomicFunction<T1, T2, T3>>(name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetFunction<T1, T2, T3>(
            this IAtomicEntity it,
            string name,
            out IAtomicFunction<T1, T2, T3> result
        )
        {
            return it.TryGet(name, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicAction GetAction(this IAtomicEntity it, string name)
        {
            return it.Get<IAtomicAction>(name);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAction(this IAtomicEntity it, string name, out IAtomicAction result)
        {
            return it.TryGet(name, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicAction<T> GetAction<T>(this IAtomicEntity it, string name)
        {
            return it.Get<IAtomicAction<T>>(name);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAction<T>(this IAtomicEntity it, string name, out IAtomicAction<T> result)
        {
            return it.TryGet(name, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicAction<T1, T2> GetAction<T1, T2>(this IAtomicEntity it, string name)
        {
            return it.Get<IAtomicAction<T1, T2>>(name);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAction<T1, T2>(this IAtomicEntity it, string name, out IAtomicAction<T1, T2> result)
        {
            return it.TryGet(name, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CallAction(this IAtomicEntity it, string name)
        {
            it.GetAction(name)?.Invoke();
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CallAction<T>(this IAtomicEntity it, string name, T args)
        {
            it.GetAction<T>(name)?.Invoke(args);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T InvokeFunction<T>(this IAtomicEntity it, string name)
        {
            IAtomicFunction<T> function = it.GetFunction<T>(name);
            if (function != null)
            {
                return function.Invoke();
            }

            return default;
        }

        public static IAtomicSetter<T> GetSetter<T>(this IAtomicEntity it, string name)
        {
            return it.Get<IAtomicSetter<T>>(name);
        }

        public static void SetValue<T>(this IAtomicEntity it, string name, T value)
        {
            if (it.TryGet(name, out IAtomicSetter<T> setter))
            {
                setter.Value = value;
            }
        }

        public static IAtomicExpression<T> GetExpression<T>(this IAtomicEntity it, string name)
        {
            return it.Get<IAtomicExpression<T>>(name);
        }
        
        public static bool TryGetExpression<T>(this IAtomicEntity it, string name, out IAtomicExpression<T> result)
        {
            return it.TryGet(name, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicObservable GetObservable(this IAtomicEntity it, string name)
        {
            return it.Get<IAtomicObservable>(name);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetObservable(this IAtomicEntity it, string name, out IAtomicObservable result)
        {
            return it.TryGet(name, out result) && result != null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicObservable<T> GetObservable<T>(this IAtomicEntity it, string name)
        {
            return it.Get<IAtomicObservable<T>>(name);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetObservable<T>(this IAtomicEntity it, string name, out IAtomicObservable<T> result)
        {
            return it.TryGet(name, out result) && result != null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ListenEvent(this IAtomicEntity it, string name, Action listener)
        {
            if (it.TryGetObservable(name, out IAtomicObservable observable))
            {
                observable.Subscribe(listener);
                return true;
            }

            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ListenEvent(this IAtomicEntity it, string name, IAtomicAction listener)
        {
            if (it.TryGetObservable(name, out IAtomicObservable observable))
            {
                observable.Subscribe(listener);
                return true;
            }

            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool UnlistenEvent(this IAtomicEntity it, string name, Action listener)
        {
            if (it.TryGetObservable(name, out IAtomicObservable observable))
            {
                observable.Unsubscribe(listener);
                return true;
            }

            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool UnlistenEvent(this IAtomicEntity it, string name, IAtomicAction listener)
        {
            if (it.TryGetObservable(name, out IAtomicObservable observable))
            {
                observable.Unsubscribe(listener);
                return true;
            }

            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ListenEvent<T>(this IAtomicEntity it, string name, Action<T> listener)
        {
            if (it.TryGetObservable(name, out IAtomicObservable<T> observable))
            {
                observable.Subscribe(listener);
                return true;
            }

            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ListenEvent<T>(this IAtomicEntity it, string name, IAtomicAction<T> listener)
        {
            if (it.TryGetObservable(name, out IAtomicObservable<T> observable))
            {
                observable.Subscribe(listener);
                return true;
            }

            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool UnlistenEvent<T>(this IAtomicEntity it, string name, Action<T> listener)
        {
            if (it.TryGetObservable(name, out IAtomicObservable<T> observable))
            {
                observable.Unsubscribe(listener);
                return true;
            }

            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool UnlistenEvent<T>(this IAtomicEntity it, string name, IAtomicAction<T> listener)
        {
            if (it.TryGetObservable(name, out IAtomicObservable<T> observable))
            {
                observable.Unsubscribe(listener);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddLogicRange(this IMutalbleAtomicBehaviour it, IEnumerable<IAtomicLogic> targets)
        {
            foreach (var target in targets)
            {
                it.AddLogic(target);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddLogicRange(this IMutalbleAtomicBehaviour it, params IAtomicLogic[] logics)
        {
            for (int i = 0, count = logics.Length; i < count; i++)
            {
                it.AddLogic(logics[i]);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveAllLogic<T>(this IMutalbleAtomicBehaviour it) where T : IAtomicLogic
        {
            cache.Clear();
            cache.AddRange(it.AllLogicUnsafe());

            for (int i = 0; i < cache.Count; i++)
            {
                if (cache[i] is T logic)
                {
                    it.RemoveLogic(logic);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RemoveLogic<T>(this IMutalbleAtomicBehaviour it) where T : IAtomicLogic
        {
            IList<IAtomicLogic> elements = it.AllLogicUnsafe();

            for (int i = 0, count = elements.Count; i < count; i++)
            {
                if (elements[i] is T element)
                {
                    it.RemoveLogic(element);
                    return true;
                }
            }

            return false;
        }

        public static bool FindLogic<T>(this IAtomicBehaviour it, out T result) where T : IAtomicLogic
        {
            IList<IAtomicLogic> elements = it.AllLogicUnsafe();

            for (int i = 0, count = elements.Count; i < count; i++)
            {
                if (elements[i] is T tElement)
                {
                    result = tElement;
                    return true;
                }
            }

            result = default;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddObject(this IMutableAtomicObject it, string name, IAtomicLogic value)
        {
            if (it.AddData(name, value))
            {
                it.AddLogic(value);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveObject(this IMutableAtomicObject it, string name)
        {
            if (it.RemoveData(name, out var value) && value is IAtomicLogic logic)
            {
                it.RemoveLogic(logic);
            }
        }
    }
}