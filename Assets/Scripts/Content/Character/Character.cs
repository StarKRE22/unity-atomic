using System;
using Atomic.Elements;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Sample
{
    [Tags(TagAPI.Character)]
    [RequireComponent(typeof(AtomicObject))]
    public sealed class Character : MonoBehaviour, IAtomicObject.IComposable, IDisposable
    {
        #region Interface

        [Reference(CommonAPI.GameObject)]
        public GameObject GameObject => this.gameObject;

        [Reference(CommonAPI.Transform)]
        public Transform Transform => this.transform;

        [Reference(CommonAPI.Rigidbody2D)]
        public Rigidbody2D Rigidbody => this.GetComponent<Rigidbody2D>();

        [Reference(CommonAPI.EffectHolder)]
        public EffectHolder EffectHolder => this.effectHolder;

        [Reference(CommonAPI.CollectCoinEvent)]
        public IAtomicObservable CollectCoinEvent => this.collectCoinEvent;

        [Reference(CommonAPI.DeathAction)]
        public IAtomicAction DeathAction => this.deathAction;

        [Reference(CommonAPI.MoveComponent)]
        public MoveComponent MoveComponent => this.moveComponent;

        [Reference(CommonAPI.JumpComponent)]
        public JumpComponent JumpComponent => this.jumpComponent;

        #endregion

        #region Core

        [SerializeField]
        private AtomicAction deathAction;

        [SerializeField]
        private AtomicVariable<bool> isAlive = new(true);

        [SerializeField]
        internal AtomicFunction<bool> isGroundMoving;

        [SerializeField]
        internal AtomicFunction<float> verticalSpeed;

        [SerializeField]
        private AtomicEvent collectCoinEvent;

        [Behaviour]
        [SerializeField]
        private MoveComponent moveComponent;

        [SerializeField]
        public JumpComponent jumpComponent;

        [Behaviour]
        [SerializeField]
        private GroundedComponent groundedComponent;

        [SerializeField]
        private EffectHolder effectHolder;

        [Behaviour]
        [SerializeField]
        private CollectCoinMechanics collectCoinMechanics;

        [Behaviour]
        private FlipMechanics flipMechanics;

        public void Compose(IAtomicObject obj)
        {
            this.moveComponent.Enabled.Append(this.isAlive);
            this.jumpComponent.Enabled.AppendAll(this.isAlive, this.groundedComponent.isGrounded);

            this.deathAction.Compose(this.Death);
            this.isGroundMoving.Compose(this.IsGroundMoving);

            this.collectCoinMechanics.Compose(this.collectCoinEvent);
            this.flipMechanics = new FlipMechanics(this.moveComponent.CurrentDirection, this.transform);

            Rigidbody2D rigidbody = this.GetComponent<Rigidbody2D>();
            this.verticalSpeed.Compose(() => rigidbody.velocity.y);

            this.effectHolder.Compose(obj);
        }

        public void Dispose()
        {
            isAlive?.Dispose();
            collectCoinEvent?.Dispose();
            jumpComponent?.Dispose();
            moveComponent?.Dispose();
        }

        private void Death()
        {
            this.isAlive.Value = false;
            this.gameObject.SetActive(false);
        }

        private bool IsGroundMoving()
        {
            return this.moveComponent.IsMoving.Value &&
                   this.groundedComponent.isGrounded.Value;
        }

        #endregion
    }
}