using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Sample
{
    public sealed class EnemyPirate : MonoBehaviour, IAtomicObject.IComposable
    {
        [Reference(CommonAPI.Rigidbody2D)]
        public Rigidbody2D Rigidbody2D => this.GetComponent<Rigidbody2D>();

        [Reference(CommonAPI.GameObject)]
        public GameObject GameObject => this.gameObject;

        [Reference(CommonAPI.Transform)]
        public Transform Transform => this.transform;

        [Reference(CommonAPI.MoveComponent)]
        [Behaviour]
        public MoveComponent moveComponent;
        
        [Behaviour]
        public GroundedComponent groundedComponent;

        [Reference(CommonAPI.EffectHolder)]
        public EffectHolder effectHolder;

        [Behaviour]
        private KillCharacterMechanics killMechanics;

        [Behaviour]
        private DiscardCharacterEffectsMechanics discardEffectsMechanics;

        public void Compose(IAtomicObject obj)
        {
            this.effectHolder.Compose(obj);
            this.killMechanics = new KillCharacterMechanics();
            this.discardEffectsMechanics = new DiscardCharacterEffectsMechanics();
        }
    }
}