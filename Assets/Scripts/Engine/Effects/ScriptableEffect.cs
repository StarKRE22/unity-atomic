using Atomic.Objects;
using UnityEngine;

namespace Sample
{
    public abstract class ScriptableEffect : ScriptableObject, IEffect
    {
        public abstract void Apply(IAtomicObject obj);
        public abstract void Discard(IAtomicObject obj);
    }
}