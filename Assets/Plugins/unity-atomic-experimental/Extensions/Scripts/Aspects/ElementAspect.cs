using System;
using Atomic.Objects;

namespace Atomic.Extensions
{
    [Serializable]
    public class ElementAspect<T> : ValueAspect<T> where T : ILogic
    {
        public override void Apply(IObject obj)
        {
            base.Apply(obj);
            obj.AddLogic(this.value);
        }

        public override void Discard(IObject obj)
        {
            base.Discard(obj);
            obj.DelLogic(this.value);
        }
    }
}