using Atomic;
using UnityEngine;

namespace Game.Engine
{
    public sealed class DamageTriggerMechanics
    {
        private readonly IAtomicAction<IAtomicObject> dealDamageAction;
        private readonly IAtomicAction<IAtomicObject> triggerEvent;

        public DamageTriggerMechanics(
            IAtomicAction<IAtomicObject> dealDamageAction,
            IAtomicAction<IAtomicObject> triggerEvent
        )
        {
            this.dealDamageAction = dealDamageAction;
            this.triggerEvent = triggerEvent;
        }

        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent(out IAtomicObject atomicObject))
            {
                this.dealDamageAction.Invoke(atomicObject);
                this.triggerEvent?.Invoke(atomicObject);
            }
        }
    }
}