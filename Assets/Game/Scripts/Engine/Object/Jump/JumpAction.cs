using System;
using Atomic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Engine
{
    [Serializable, InlineProperty]
    public sealed class JumpAction : IAtomicAction
    {
        private Rigidbody2D rigidbody;
        private IAtomicValue<float> force;
        private IAtomicValue<bool> enabled;
        private IAtomicEvent jumpEvent;

        public JumpAction()
        {
        }

        public JumpAction(Rigidbody2D rigidbody, IAtomicValue<float> force, IAtomicValue<bool> enabled, IAtomicEvent jumpEvent)
        {
            this.Compose(rigidbody, force, enabled, jumpEvent);
        }

        public void Compose(
            Rigidbody2D rigidbody,
            IAtomicValue<float> force,
            IAtomicValue<bool> enabled,
            IAtomicEvent jumpEvent
        )
        {
            this.rigidbody = rigidbody;
            this.force = force;
            this.enabled = enabled;
            this.jumpEvent = jumpEvent;
        }

        [Button]
        public void Invoke()
        {
            if (!this.enabled.Value)
            {
                return;
            }

            this.rigidbody.AddForce(new Vector2(0, this.force.Value), ForceMode2D.Impulse);
            this.jumpEvent?.Invoke();
        }
    }
}