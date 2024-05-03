using Atomic;
using Game.Engine;
using UnityEngine;

namespace Game.Content
{
    public sealed class Bullet : AtomicObject
    {
        [SerializeField]
        private AtomicValue<int> damage = new(1);

        [SerializeField]
        private DealDamageAction dealDamageAction = new();

        [SerializeField]
        private MoveComponent moveComponent;

        [SerializeField]
        private AtomicAction<IAtomicObject> deathEvent;

        private DamageTriggerMechanics damageTriggerMechanics;

        [SerializeField]
        private Countdown countdown;

        private void Awake()
        {
            this.moveComponent.Compose();
            this.moveComponent.Direction.Value = Mathf.Sign(this.transform.right.x);

            this.dealDamageAction.Compose(this.damage);
            this.damageTriggerMechanics = new DamageTriggerMechanics(this.dealDamageAction, this.deathEvent);
            this.deathEvent.Compose(_ => Destroy(this.gameObject));
        }

        private void FixedUpdate()
        {
            float deltaTime = Time.fixedDeltaTime;
            
            this.moveComponent.OnFixedUpdate(deltaTime);
            this.countdown.Tick(deltaTime);

            if (this.countdown.IsEnded())
            {
                Destroy(this.gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            this.damageTriggerMechanics.OnTriggerEnter2D(other);
        }
    }
}