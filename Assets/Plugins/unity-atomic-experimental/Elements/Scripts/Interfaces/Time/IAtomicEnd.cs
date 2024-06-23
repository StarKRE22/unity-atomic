using System;

namespace Atomic.Elements
{
    public interface IAtomicEnd
    {
        event Action OnEnded;
        bool IsEnded();
    }
}