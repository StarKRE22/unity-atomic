using System;

namespace Atomic.Elements
{
    public interface IAtomicProgress : IAtomicTime, IAtomicDuration
    {
        event Action<float> OnProgressChanged; 

        float GetProgress();
        void SetProgress(float progress);
    }
}