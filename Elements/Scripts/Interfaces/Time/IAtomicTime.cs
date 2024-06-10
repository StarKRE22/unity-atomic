using System;

namespace Atomic.Elements
{
    public interface IAtomicTime
    {
        event Action<float> OnTimeChanged;
        
        float GetTime();
        void SetTime(float time);
    }
}