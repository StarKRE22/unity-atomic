using Atomic.Objects;
using UnityEngine;

namespace Atomic.Extensions
{
    public abstract class ScriptableAspectBase : ScriptableObject, IAspect
    {
        public abstract void Apply(IObject obj);
        public abstract void Discard(IObject obj);
    }
}