using System;

namespace Atomic.Elements
{
    public interface IAtomicCountdown : IAtomicPlayable, IAtomicResetable
    {
        event Action OnTimeChanged;
        event Action OnEnded;

        float Progress { get; set; }
        float Duration { get; set; }
        float RemainingTime { get; set; }
    }
}