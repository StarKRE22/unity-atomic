using System.Collections.Generic;
using System.Runtime.CompilerServices;
// ReSharper disable ForCanBeConvertedToForeach

namespace Atomic
{
    public static class ObjectExtensions
    {
        private static readonly List<IAtomicLogic> cache = new();

        public static bool FindLogic<T>(this IAtomicObject it, out T result) where T : IAtomicLogic
        {
            var elements = it.AllLogicReadOnly();

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
        public static void AddLogicRange(this IAtomicObject it, IEnumerable<IAtomicLogic> targets)
        {
            foreach (var target in targets)
            {
                it.AddLogic(target);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddLogicRange(this IAtomicObject it, params IAtomicLogic[] logics)
        {
            for (int i = 0, count = logics.Length; i < count; i++)
            {
                it.AddLogic(logics[i]);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveAllLogic<T>(this IAtomicObject it) where T : IAtomicLogic
        {
            cache.Clear();
            cache.AddRange(it.AllLogicReadOnly());

            for (int i = 0; i < cache.Count; i++)
            {
                if (cache[i] is T logic)
                {
                    it.RemoveLogic(logic);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool RemoveLogic<T>(this IAtomicObject it) where T : IAtomicLogic
        {
            var elements = it.AllLogicReadOnly();

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddElement(this IAtomicObject it, string name, IAtomicLogic value)
        {
            if (it.AddField(name, value))
            {
                it.AddLogic(value);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddElement(this IAtomicObject it, int index, IAtomicLogic value)
        {
            if (it.AddField(index, value))
            {
                it.AddLogic(value);
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveElement(this IAtomicObject it, string name)
        {
            if (it.RemoveField(name, out var value) && value is IAtomicLogic logic)
            {
                it.RemoveLogic(logic);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveElement(this IAtomicObject it, int index)
        {
            if (it.RemoveField(index, out var value) && value is IAtomicLogic logic)
            {
                it.RemoveLogic(logic);
            }
        }
    }
}