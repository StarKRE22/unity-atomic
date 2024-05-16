using System;
using System.Collections.Generic;
using System.Linq;
using Atomic.Elements;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Sample
{
    [Serializable]
    public sealed class JumpAspect : ScriptableObject, IAtomicObject.IComposable, IAtomicObject.IDisposable
    {
        [SerializeField]
        public float baseForce;

        private readonly IEnumerable<IAtomicFunction<IAtomicObject, bool>> conditions;

        public JumpAspect(params IAtomicFunction<IAtomicObject, bool>[] conditions)
        {
            this.conditions = conditions;
        }

        public void Compose(IAtomicObject target)
        {
            if (target.TryGetRigidbody2D(out Rigidbody2D rigidbody2D))
            {
                var jumpComponent = new JumpComponent(rigidbody2D, baseForce);
                // jumpComponent.Enabled.Append(new AtomicFunction<bool>()); //Conditions
                target.AddReference(CommonAPI.JumpComponent, jumpComponent);
            }
        }

        public void Dispose(IAtomicObject target)
        {
            target.DelReference(CommonAPI.JumpComponent);
        }
    }
}