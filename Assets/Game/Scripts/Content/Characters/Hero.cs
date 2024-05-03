using System;
using Atomic;
using Game.Engine;
using UnityEngine;

namespace Game.Content
{
    [Is(TypeAPI.Character)]
    public sealed class Hero : AtomicObject
    {
        #region API

        [Get(MasterAPI.Transform)]
        public Transform Transform => this.core.transform;

        [Get(MasterAPI.Rigidbody)]
        public Rigidbody2D Rigidbody => this.core.rigidbody;

        [Get(MovementAPI.MoveDirection)]
        public IAtomicVariable<float> MoveDirection => this.core.moveComponent.Direction;

        [Get(CommonAPI.CollectCoinEvent)]
        public IAtomicObservable CollectCoinEvent => this.core.collectCoinEvent;

        [Get(HealthAPI.DeathAction)]
        public IAtomicAction DeathAction => this.core.deathAction;

        [Get(MasterAPI.EffectManager)]
        public EffectManager EffectManager => this.core.effectManager;

        #endregion
        
        #region CORE

        [SerializeField]
        private Character_Core core;

        [SerializeField]
        private Character_View view;

        private void Awake()
        {
            this.core.Compose(this);
            this.view.Compose(this.core);
        }

        private void OnEnable()
        {
            this.Enable();
            this.view.OnEnable();
        }

        private void OnDisable()
        {
            this.Disable();
            this.view.OnDisable();
        }

        private void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;
            this.OnFixedUpdate(deltaTime);
            this.core.OnFixedUpdate(deltaTime);
            // this.ai.OnFixedUpdate(deltaTime);
        }

        private void Update()
        {
            OnUpdate(Time.deltaTime);
            this.view.Update();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            this.core.OnTriggerEnter2D(col);
        }

        private void OnDrawGizmos()
        {
            this.core.OnDrawGizmos();
        }

        private void OnDestroy()
        {
            this.core.Dispose();
        }

        #endregion
    }
    
    [Serializable]
    public sealed class Character_Core : IAtomicFixedUpdate, IDisposable
    {
        public Transform transform;
        public Rigidbody2D rigidbody;
        public GameObject gameObject;
        
        public AtomicEvent collectCoinEvent;
        public AtomicVariable<bool> isAlive = new(true);
        public AtomicFunction<bool> isGroundMoving;
        public AtomicFunction<float> speedY;
        public AtomicAction deathAction;
        
        public MoveComponent moveComponent;
        public JumpComponent jumpComponent;
        public GroundedComponent groundedComponent;
        
        public EffectManager effectManager;

        private CollectCoinMechanics collectCoinMechanics;
        
        public void Compose(AtomicObject character)
        {
            this.moveComponent.Let(it =>
            {
                it.Compose();
                it.Enabled.Append(this.isAlive);
            });
            
            this.jumpComponent.Let(it =>
            {
                it.Compose();
                it.JumpEnabled.Append(this.isAlive);
                it.JumpEnabled.Append(this.groundedComponent.IsGrounded);
            });

            this.groundedComponent.Compose();
            
            this.deathAction.Compose(() =>
            {
                this.isAlive.Value = false;
                this.gameObject.SetActive(false);
            });

            this.collectCoinMechanics = new CollectCoinMechanics(this.collectCoinEvent);


            this.isGroundMoving.Compose(() => this.moveComponent.IsMoving.Value &&
                                              this.groundedComponent.IsGrounded.Value);

            this.speedY.Compose(() => this.rigidbody.velocity.y);

            this.effectManager.Compose(character);
        }

        public void OnTriggerEnter2D(Collider2D col)
        {
            this.collectCoinMechanics.OnTriggerEnter2D(col);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            this.moveComponent.OnFixedUpdate(deltaTime);
            this.groundedComponent.OnFixedUpdate(deltaTime);
        }

        public void OnDrawGizmos()
        {
            this.groundedComponent.OnDrawGizmos();
        }

        public void Dispose()
        {
            this.isAlive?.Dispose();
            this.moveComponent?.Dispose();
            this.collectCoinEvent?.Dispose();
        }
    }
    
    [Serializable]
    public sealed class Character_View
    {
        [SerializeField]
        private Animator animator;

        [Get(MasterAPI.SpriteRenderer)]
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private MoveAnimMechanics moveMechanics;
        private JumpAnimMechanics jumpMechanics;
        private FlyAnimMechanics flyMechanics;

        public void Compose(Character_Core core)
        {
            this.moveMechanics = new MoveAnimMechanics(this.animator, core.isGroundMoving);
            this.jumpMechanics = new JumpAnimMechanics(this.animator, core.jumpComponent.JumpEvent);
            this.flyMechanics = new FlyAnimMechanics(this.animator, core.speedY);
        }

        public void OnEnable()
        {
            this.jumpMechanics.OnEnable();
        }

        public void OnDisable()
        {
            this.jumpMechanics.OnDisable();
        }

        public void Update()
        {
            this.moveMechanics.Update();
            this.flyMechanics.Update();
        }
    }
}