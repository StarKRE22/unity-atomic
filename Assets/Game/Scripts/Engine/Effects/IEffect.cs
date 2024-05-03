using System;
using Atomic;

namespace Game.Engine
{
    public interface IEffect
    {
        void Apply(IAtomicObject obj);
        
        void Discard(IAtomicObject obj);
    }

    public interface ICompletableEffect : IEffect
    {
        protected internal void SetCallback(Action<IEffect> callback);
    }
}