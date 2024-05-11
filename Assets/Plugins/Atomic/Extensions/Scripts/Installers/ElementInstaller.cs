using System;
using Atomic.Objects;

namespace Atomic
{
    [Serializable]
    public class ElementInstaller<T> : ReferenceInstaller<T> where T : IAtomicObject.IBehaviour
    {
        public override void Compose(IAtomicObject obj)
        {
            base.Compose(obj);
            obj.AddBehaviour(this.value);
        }
    }
}