using System;
using Atomic.Objects;
using GameEngine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sample
{
    [Serializable, InlineProperty]
    public sealed class JumpComponentInstaller : IComposable
    {
        [SerializeField]
        private JumpComponent jumpComponent;

        public JumpComponentInstaller()
        {
            jumpComponent = new JumpComponent(null, 0);
        }

        public void Compose(IAtomicObject obj)
        {
            obj.AddValue(CommonAPI.JumpComponent, this.jumpComponent);
        }
    }
}