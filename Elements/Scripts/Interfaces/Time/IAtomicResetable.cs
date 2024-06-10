using System;

namespace Atomic.Elements
{
    public interface IAtomicResetable
    {
        event Action OnReset;
        void Reset();
    }
}