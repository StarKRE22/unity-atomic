using System;
using Atomic.Objects;

namespace Sample
{
    public interface IEffect
    {
        void Apply(IObject obj);
        void Discard(IObject obj);
    }

    public interface ICompletableEffect : IEffect
    {
        protected internal void SetCallback(Action<IEffect> callback);
    }
}