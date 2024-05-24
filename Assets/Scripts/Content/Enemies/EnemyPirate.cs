using Atomic.Objects;
using GameEngine;
using UnityEngine;
using GameObject = UnityEngine.GameObject;

namespace Sample
{
    public sealed class EnemyPirate : MonoBehaviour, IComposable
    {
        [Value(CommonAPI.Rigidbody2D)]
        public Rigidbody2D Rigidbody2D => this.GetComponent<Rigidbody2D>();

        [Value(CommonAPI.GameObject)]
        public GameObject GameObject => this.gameObject;

        [Value(CommonAPI.Transform)]
        public Transform Transform => this.transform;

        [Value(CommonAPI.MoveComponent)]
        [Logic]
        public MoveComponent moveComponent;
        
        [Logic]
        public GroundedComponent groundedComponent;

        [Value(CommonAPI.EffectHolder)]
        public EffectHolder effectHolder;

        [Logic]
        private KillCharacterMechanics killMechanics;

        [Logic]
        private DiscardCharacterEffectsMechanics discardEffectsMechanics;

        public void Compose(IAtomicObject obj)
        {
            this.effectHolder.Compose(obj);
            this.killMechanics = new KillCharacterMechanics();
            this.discardEffectsMechanics = new DiscardCharacterEffectsMechanics();
        }
    }
}