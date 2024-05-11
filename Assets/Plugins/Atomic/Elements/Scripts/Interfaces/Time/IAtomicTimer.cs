using System;

namespace Atomic.Elements
{
    public interface IAtomicTimer : IAtomicPlayable, IAtomicResetable
    {
        event Action OnTimeChanged;
        event Action OnFinished;
        
        float Progress { get; set; }
        float Duration { get; set; }
        float CurrentTime { get; set; }
    }
}