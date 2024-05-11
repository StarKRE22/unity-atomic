using System;

namespace Atomic.Elements
{
    public interface IAtomicPlayable
    {
        event Action OnStarted;
        event Action OnStopped;

        bool IsPlaying { get; }
        
        void Start();
        void Stop();
    }
}