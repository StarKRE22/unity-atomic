using System.Runtime.CompilerServices;
using Atomic.Objects;

namespace Atomic.Extensions
{
    public static class AspectExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ApplyAspect(this IObject obj, IAspect aspect)
        {
            aspect.Apply(obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DiscardAspect(this IObject obj, IAspect aspect)
        {
            aspect.Discard(obj);
        }
    }
}