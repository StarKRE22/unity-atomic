using System;

namespace Atomic.Elements
{
    public interface IAtomicDuration
    {
        event Action<float> OnDurationChanged; 

        float GetDuration();
        void SetDuration(float duration);
    }
}