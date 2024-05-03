using Atomic;
using UnityEngine;

namespace Game.Engine
{
    public abstract class ScriptableEffect : ScriptableObject, IEffect
    {
        public abstract void Apply(IAtomicObject obj);
        public abstract void Discard(IAtomicObject obj);
    }
}