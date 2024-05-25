using System;
using Atomic.Elements;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Sample
{
    [Tags(TagAPI.Character)]
    public sealed class Character : MonoBehaviour, IAwake, IDisposable
    {
        #region Interface

        [Value(CommonAPI.GameObject)]
        public GameObject GameObject => this.gameObject;

        [Value(CommonAPI.Transform)]
        public Transform Transform => this.transform;

        [Value(CommonAPI.Rigidbody2D)]
        public Rigidbody2D Rigidbody => this.GetComponent<Rigidbody2D>();

        [Value(CommonAPI.EffectHolder)]
        public EffectHolder EffectHolder => this.effectHolder;

        [Value(CommonAPI.CollectCoinEvent)]
        public IAtomicObservable CollectCoinEvent => this.collectCoinEvent;

        [Value(CommonAPI.DeathAction)]
        public IAtomicAction DeathAction => this.deathAction;

        [Value(CommonAPI.MoveComponent)]
        public MoveComponent MoveComponent => this.moveComponent;

        [Value(CommonAPI.JumpComponent)]
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

        [Logic]
        [SerializeField]
        private MoveComponent moveComponent;

        [SerializeField]
        internal JumpComponent jumpComponent;

        [Logic]
        [SerializeField]
        private GroundedComponent groundedComponent;

        [SerializeField]
        private EffectHolder effectHolder;

        [Logic]
        [SerializeField]
        private CollectCoinMechanics collectCoinMechanics;

        [Logic]
        [SerializeField]
        private FlipMechanics flipMechanics;
        
        public void OnAwake(IAtomicObject obj)
        {
            this.moveComponent.Enabled.Append(this.isAlive);
            this.jumpComponent.Enabled.AppendAll(this.isAlive, this.groundedComponent.isGrounded);

            this.deathAction.Compose(this.Death);
            this.isGroundMoving.Compose(this.IsGroundMoving);

            this.collectCoinMechanics.Compose(this.collectCoinEvent);
            this.flipMechanics.Compose(this.moveComponent.CurrentDirection, this.transform);

            Rigidbody2D rigidbody = this.GetComponent<Rigidbody2D>();
            this.verticalSpeed.Compose(() => rigidbody.velocity.y);

            this.effectHolder.Compose(obj);
        }
        
        public void Dispose()
        {
            this.isAlive?.Dispose();
            this.collectCoinEvent?.Dispose();
            this.moveComponent?.Dispose();
            this.jumpComponent?.Dispose();
            this.groundedComponent?.Dispose();
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