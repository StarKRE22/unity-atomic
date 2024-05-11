using System;

namespace Atomic.Elements
{
    public interface IAtomicStopwatch : IAtomicPlayable, IAtomicResetable
    {
        event Action OnTimeChanged;
        float CurrentTime { get; set; }
    }
}