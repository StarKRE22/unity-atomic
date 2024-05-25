using System;
using Atomic.Objects;
using GameEngine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sample
{
    [Serializable, InlineProperty]
    public sealed class JumpAspect : IAspect
    {
        [SerializeField]
        private JumpComponent jumpComponent;
        
        public void Compose(IAtomicObject obj)
        {
            obj.AddValue(CommonAPI.JumpComponent, this.jumpComponent);
        }

        public void Dispose(IAtomicObject obj)
        {
            obj.DelValue(CommonAPI.JumpComponent);
        }
    }
}