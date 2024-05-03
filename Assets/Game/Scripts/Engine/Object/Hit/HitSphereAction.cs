using System;
using Atomic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class HitSphereAction : IAtomicAction
    {
        private static readonly Collider2D[] buffer = new Collider2D[32];

        private IAtomicFunction<IAtomicObject, bool> dealDamageCondition;
        private IAtomicAction<IAtomicObject> dealDamageAction;
        private IAtomicValue<Vector2> hitCenter;
        private IAtomicValue<float> hitRadius;
        private IAtomicValue<LayerMask> layerMask;

        public void Compose(
            IAtomicFunction<IAtomicObject, bool> dealDamageCondition,
            IAtomicAction<IAtomicObject> dealDamageAction,
            IAtomicValue<Vector2> hitPoint,
            IAtomicValue<float> hitRadius,
            IAtomicValue<LayerMask> layerMask
        )
        {
            this.dealDamageCondition = dealDamageCondition;
            this.dealDamageAction = dealDamageAction;
            this.hitCenter = hitPoint;
            this.hitRadius = hitRadius;
            this.layerMask = layerMask;
        }

        [Button]
        public void Invoke()
        {
            var hitPosition = this.hitCenter.Value;
            var hitRadius = this.hitRadius.Value;
            
            var size = Physics2D.OverlapCircleNonAlloc(hitPosition, hitRadius, buffer, this.layerMask.Value);

            for (int i = 0; i < size; i++)
            {
                Collider2D collider = buffer[i];
                if (collider.TryGetComponent(out IAtomicObject target) && this.dealDamageCondition.Invoke(target))
                {
                    this.dealDamageAction.Invoke(target);
                    return;
                }
            }
        }
    }
}