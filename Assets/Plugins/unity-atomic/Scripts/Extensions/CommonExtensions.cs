using System;
using System.Runtime.CompilerServices;

namespace Atomic
{
    public static class CommonExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Let<T>(this T it, Action<T> let)
        {
            let.Invoke(it);
        }
    }
}