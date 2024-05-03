using System;
using AIModule;
using Atomic;
using Game.Engine;
using UnityEngine;

namespace Game.Content
{
    [Is(TypeAPI.Moveable, TypeAPI.Damagable)]
    public sealed class EnemyPirate : AtomicObject
    {
        #region API

        ///Common 
        [Get(MasterAPI.Rigidbody)]
        public Rigidbody2D Rigidbody => this.core.rigidbody2D;

        [Get(MasterAPI.Transform)]
        public Transform Transform => this.core.transform;
        
        ///Life 
        [Get(HealthAPI.IsAlive)]
        public IAtomicValue<bool> IsAlive => this.core.healthComponent.IsAlive;

        [Get(HealthAPI.TakeDamageAction)]
        public IAtomicAction<int> TakeDamageAction => this.core.healthComponent.TakeDamageAction;
        
        ///Move 
        [Get(MovementAPI.MoveDirection)]
        public IAtomicVariable<float> MoveDirection => this.core.moveComponent.Direction;

        [Get(CommonAPI.LookDirection)]
        public IAtomicVariable<float> FlipDirection => this.core.flipDirection;
        
        ///Attack 
        [Get(AttackAPI.AttackRequest)]
        public IAtomicAction AttackRequest => this.core.attackComponent.AttackRequest;
        
        ///View 
        [Get(MasterAPI.Animator)]
        public Animator Animator => this.view.animator;

        [Get(MasterAPI.SpriteRenderer)]
        public SpriteRenderer SpriteRenderer => this.view.spriteRenderer;

        #endregion

        #region CORE

        [SerializeField]
        private PirateEnemy_Core core;
        
        [SerializeField]
        private PirateEnemy_View view;

        [SerializeField]
        private PirateEnemy_AI ai;

        private void Awake()
        {
            this.core.Compose(this);
            this.view.Compose(this.core);
            this.ai.Compose(this);
        }

        private void OnEnable()
        {
            this.Enable();
            this.core.Enable();
            this.view.Enable();
            this.ai.Enable();
        }

        private void OnDisable()
        {
            this.Disable();
            this.core.Disable();
            this.view.Disable();
            this.ai.Disable();
        }

        private void FixedUpdate()
        {
            float fixedDeltaTime = Time.fixedDeltaTime;
            this.OnFixedUpdate(fixedDeltaTime);
            this.core.OnFixedUpdate(fixedDeltaTime);
            this.ai.OnFixedUpdate(fixedDeltaTime);
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            this.OnUpdate(deltaTime);
            this.view.OnUpdate(deltaTime);
        }

        private void OnDrawGizmos()
        {
            this.ai.OnDrawGizmos();
        }

        #endregion
    }

    [Serializable]
    public sealed class PirateEnemy_Core : IAtomicFixedUpdate, IAtomicEnable, IAtomicDisable
    {
        [Get(MasterAPI.Rigidbody)]
        public Rigidbody2D rigidbody2D;

        [Get(MasterAPI.Transform)]
        public Transform transform;

        public HealthComponent healthComponent;
        public GroundedComponent groundedComponent;
        public MoveComponent moveComponent;
        public AttackComponent attackComponent;

        public MeleeWeapon meleeWeapon;

        [SerializeField]
        public AtomicVariable<float> flipDirection;

        private FlipMechanics flipMechanics;

        private FlipTowardsMoveMechanics lookMechanics;

        public void Compose(AtomicObject obj)
        {
            this.healthComponent.Let(it =>
            {
                it.Compose();
                it.DeathEvent.Subscribe(() => GameObject.Destroy(obj.gameObject));
            });

            this.groundedComponent.Compose();

            this.moveComponent.Let(it =>
            {
                it.Enabled.Append(this.healthComponent.IsAlive);
                it.Compose();
            });

            this.attackComponent.Let(it =>
            {
                it.Compose();
                it.AttackCondition.Append(this.healthComponent.IsAlive);
                it.AttackCondition.Append(this.moveComponent.IsMoving.AsNot());
            });

            this.flipMechanics = new FlipMechanics(this.flipDirection, this.transform);
            this.lookMechanics = new FlipTowardsMoveMechanics(this.moveComponent.Direction, this.flipDirection);
        }


        public void OnFixedUpdate(float deltaTime)
        {
            this.moveComponent.OnFixedUpdate(deltaTime);
            this.groundedComponent.OnFixedUpdate(deltaTime);
            this.flipMechanics.OnFixedUpdate(deltaTime);
            this.lookMechanics.OnFixedUpdate(deltaTime);
        }

        public void Enable()
        {
            this.healthComponent.OnEnable();
        }

        public void Disable()
        {
            this.healthComponent.OnDisable();
        }
    }

    [Serializable]
    public sealed class PirateEnemy_View : IAtomicEnable, IAtomicDisable, IAtomicUpdate
    {
        private const string ATTACK_ANIM_EVENT = "hit";

        [Get(MasterAPI.Animator)]
        public Animator animator;

        public AnimatorDispatcher dispatcher;

        [Get(MasterAPI.SpriteRenderer)]
        public SpriteRenderer spriteRenderer;


        private MoveAnimMechanics moveMechanics;
        private AttackAnimMechanics triggerMechanics;
        private AnimationEventListener attackListener;

        public void Compose(PirateEnemy_Core core)
        {
            this.moveMechanics = new MoveAnimMechanics(
                this.animator, core.moveComponent.IsMoving
            );

            this.triggerMechanics = new AttackAnimMechanics(
                this.animator, core.attackComponent.AttackEvent
            );
            this.attackListener = new AnimationEventListener(
                this.dispatcher, ATTACK_ANIM_EVENT, core.meleeWeapon.FireAction
            );
        }

        public void Enable()
        {
            this.triggerMechanics.Enable();
            this.attackListener.Enable();
        }

        public void Disable()
        {
            this.triggerMechanics.Disable();
            this.attackListener.Disable();
        }

        public void OnUpdate(float deltaTime)
        {
            this.moveMechanics.Update();
        }
    }

    [Serializable]
    public sealed class PirateEnemy_AI : IAtomicFixedUpdate, IAtomicEnable, IAtomicDisable
    {
        [SerializeField]
        private Blackboard blackboard;

        [SerializeField]
        private AIBehaviour behaviour;

        public void Compose(AtomicObject character)
        {
            this.blackboard.SetObject(BlackboardAPI.Character, character);
            character.AddField(nameof(Blackboard), this.blackboard);
        }

        public void Enable()
        {
            this.behaviour.OnStart();
        }

        public void Disable()
        {
            this.behaviour.OnStop();
        }

        public void OnFixedUpdate(float deltaTime)
        {
            this.behaviour.OnUpdate(deltaTime);
        }

        public void OnDrawGizmos()
        {
            this.behaviour.OnGizmos();
        }
    }
}