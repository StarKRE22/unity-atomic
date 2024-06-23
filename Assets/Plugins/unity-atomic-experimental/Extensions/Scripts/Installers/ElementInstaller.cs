using System;
using Atomic.Objects;

namespace Atomic.Extensions
{
    [Serializable]
    public class ElementInstaller<T> : ValueInstaller<T> where T : ILogic
    {
        public override void Install(IObject obj)
        {
            base.Install(obj);
            obj.AddLogic(this.value);
        }
    }
}