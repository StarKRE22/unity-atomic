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
    public sealed class JumpAspect : ScriptableObject, IAspect
    {
        [SerializeField]
        public float baseForce;

        private readonly IEnumerable<IAtomicFunction<IObject, bool>> conditions;

        public JumpAspect(params IAtomicFunction<IObject, bool>[] conditions)
        {
            this.conditions = conditions;
        }

        public void Compose(IObject target)
        {
            if (target.TryGetRigidbody2D(out Rigidbody2D rigidbody2D))
            {
                var jumpComponent = new JumpComponent(rigidbody2D, baseForce);
                // jumpComponent.Enabled.Append(new AtomicFunction<bool>()); //Conditions
                target.AddValue(CommonAPI.JumpComponent, jumpComponent);
            }
        }

        public void Dispose(IObject target)
        {
            target.DelValue(CommonAPI.JumpComponent);
        }
    }
}