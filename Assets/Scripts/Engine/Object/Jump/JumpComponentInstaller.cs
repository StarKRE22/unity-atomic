using System;
using Atomic.Objects;
using GameEngine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sample
{
    [Serializable, InlineProperty]
    public sealed class JumpComponentInstaller : IAtomicObject.IComposable
    {
        [SerializeField]
        private JumpComponent jumpComponent;

        public void Compose(IAtomicObject obj)
        {
            obj.AddReference(CommonAPI.JumpComponent, jumpComponent);
        }
    }
}