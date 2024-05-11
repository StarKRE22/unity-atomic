using System;

namespace Atomic.Objects
{
    [Serializable]
    public class AtomicElementInstaller<T> : AtomicReferenceInstaller<T> where T : IAtomicObject.IBehaviour
    {
        public override void Compose(IAtomicObject obj)
        {
            base.Compose(obj);
            obj.AddBehaviour(this.value);
        }
    }
}