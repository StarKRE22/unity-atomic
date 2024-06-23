using System.Runtime.CompilerServices;
using Atomic.Elements;
using Atomic.Objects;

namespace Atomic.Extensions
{
    public static class TimeExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddTickTimerRule(this IObject it, AtomicTimer timer, TickMode mode = TickMode.UPDATE)
        {
            it.AddLogic(new TickTimerRule(timer, mode));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddTickCountdownRule(this IObject it, AtomicCountdown timer, TickMode mode = TickMode.UPDATE)
        {
            it.AddLogic(new TickCountdownRule(timer, mode));
        }
    }
}