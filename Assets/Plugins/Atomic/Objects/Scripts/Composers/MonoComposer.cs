using UnityEngine;

namespace Atomic.Objects
{
    public abstract class MonoComposer : MonoBehaviour, IComposer
    {
        public abstract void Compose(IAtomicObject obj);
    }
}