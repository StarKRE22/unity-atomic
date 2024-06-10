using System;

namespace Atomic.Extensions
{
    public static class ScopeExtensions
    {
        public static T With<T>(this T it, Action<T> action)
        {
            action.Invoke(it);
            return it;
        }

        public static void Apply<T>(this T it, Action<T> action)
        {
            action.Invoke(it);
        }

        public static R Let<T, R>(this T it, Func<T, R> action)
        {
            return action.Invoke(it);
        }
    }
}