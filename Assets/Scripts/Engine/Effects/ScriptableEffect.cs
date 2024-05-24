using Atomic.Objects;
using UnityEngine;

namespace Sample
{
    public abstract class ScriptableEffect : ScriptableObject, IEffect
    {
        public abstract void Apply(IObject obj);
        public abstract void Discard(IObject obj);
    }
}