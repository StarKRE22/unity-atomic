using System;

namespace Atomic.Elements
{
    public interface IAtomicPeriod : IAtomicPlayable
    {
        event Action OnPeriod;
        
        float Duration { get; set; }
    }
}