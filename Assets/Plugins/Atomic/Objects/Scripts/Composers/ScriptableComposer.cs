using UnityEngine;

namespace Atomic.Objects
{
    public abstract class ScriptableComposer : ScriptableObject, IComposer
    {
        public abstract void Compose(IAtomicObject obj);
    }
}