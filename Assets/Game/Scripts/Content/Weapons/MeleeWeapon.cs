using Atomic;
using Game.Engine;
using UnityEditor;
using UnityEngine;

namespace Game.Content
{
    public sealed class MeleeWeapon : Weapon
    { 
        public override IAtomicFunction<bool> FireCondition => this.hitCondiiton;
        
        public IAtomicAction FireAction => this.hitAction;
        
        [SerializeField]
        private AtomicAnd hitCondiiton;

        [SerializeField]
        private Transform hitCenter;

        [SerializeField]
        private AtomicValue<float> hitRadius;

        [SerializeField]
        private HitSphereAction hitAction;

        [SerializeField]
        private AtomicValue<LayerMask> layerMask;

        [SerializeField]
        private AtomicVariable<int> damage;

        [SerializeField]
        private AtomicFunction<IAtomicObject, bool> damageCondition;

        [SerializeField]
        private DealDamageAction damageAction;

        private void Awake()
        {
            this.Compose();
        }

        private void Compose()
        {
            this.hitAction.Compose(
                this.damageCondition,
                this.damageAction,
                this.hitCenter.AsFunction(it => (Vector2) it.position),
                this.hitRadius,
                this.layerMask
            );

            this.damageCondition.Compose(_ => true);
            this.damageAction.Compose(this.damage);
        }

        private void OnDrawGizmos()
        {
            if (this.hitCenter != null)
            {
                Handles.color = Color.red;
                Handles.DrawWireDisc(this.hitCenter.position, Vector3.forward, this.hitRadius.Value);
            }
        }
    }
}