using System;
using AIModule;
using Atomic;
using Game.Engine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Content
{
    [Is(TypeAPI.Moveable, TypeAPI.Damagable)]
    public sealed class EnemyCaptain : AtomicObject
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

        [Get(AttackAPI.SwitchToMeleeWeapon)]
        public IAtomicAction SwitchToMeleeWeaponAction => this.core.switchToMeleeWeaponAction;

        [Get(AttackAPI.SwitchToRangeWeapon)]
        public IAtomicAction SwitchToRangeWeaponAction => this.core.switchToRangeWeaponAction;

        ///View
        [Get(MasterAPI.Animator)]
        public Animator Animator => this.view.animator;

        [Get(MasterAPI.SpriteRenderer)]
        public SpriteRenderer SpriteRenderer => this.view.spriteRenderer;

        #endregion

        #region CORE

        [SerializeField]
        private CaptainEnemy_Core core;

        [SerializeField]
        private CaptainEnemy_View view;

        [SerializeField]
        private CaptainEnemy_AI ai;

        private void Awake()
        {
            this.core.Compose(this);
            this.view.Compose(this.core);
            this.ai.Compose(this);
        }

        private void OnEnable()
        {
            this.core.Enable();
            this.view.Enable();
            this.ai.Enable();
        }

        private void OnDisable()
        {
            this.core.Disable();
            this.view.Disable();
            this.ai.Disable();
        }

        private void FixedUpdate()
        {
            float fixedDeltaTime = Time.fixedDeltaTime;
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
    public sealed class CaptainEnemy_Core : IAtomicFixedUpdate, IAtomicEnable, IAtomicDisable
    {
        public Rigidbody2D rigidbody2D;
        public Transform transform;

        public HealthComponent healthComponent;
        public GroundedComponent groundedComponent;
        public MoveComponent moveComponent;
        public AttackComponent attackComponent;

        [Header("Weapons")]
        public AtomicVariable<Weapon> currentWeapon;

        public AtomicAction switchToMeleeWeaponAction;
        public AtomicAction switchToRangeWeaponAction;

        public AtomicEvent switchToMeleeWeaponEvent;
        public AtomicEvent switchToRangeWeaponEvent;

        public Countdown switchWeaponCountdown;

        public MeleeWeapon meleeWeapon;
        public RangeWeapon rangeWeapon;

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
                it.AttackCondition.Append(new AtomicFunction<bool>(this.switchWeaponCountdown.IsEnded));
            });

            this.switchToMeleeWeaponAction.Compose(() =>
            {
                if (this.currentWeapon.Value == this.meleeWeapon)
                {
                    return;
                }

                this.currentWeapon.Value = this.meleeWeapon;
                this.switchWeaponCountdown.Reset();
                this.switchToMeleeWeaponEvent?.Invoke();
            });
            this.switchToRangeWeaponAction.Compose(() =>
            {
                if (this.currentWeapon.Value == this.rangeWeapon)
                {
                    return;
                }

                this.currentWeapon.Value = this.rangeWeapon;
                this.switchWeaponCountdown.Reset();
                this.switchToRangeWeaponEvent?.Invoke();
            });

            this.flipMechanics = new FlipMechanics(this.flipDirection, this.transform);
            this.lookMechanics = new FlipTowardsMoveMechanics(this.moveComponent.Direction, this.flipDirection);

            obj.AddField(AttackAPI.WeaponCharges, this.rangeWeapon.Charges);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            this.moveComponent.OnFixedUpdate(deltaTime);
            this.groundedComponent.OnFixedUpdate(deltaTime);
            this.flipMechanics.OnFixedUpdate(deltaTime);
            this.lookMechanics.OnFixedUpdate(deltaTime);
            this.switchWeaponCountdown.Tick(deltaTime);
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
    public sealed class CaptainEnemy_View : IAtomicEnable, IAtomicDisable, IAtomicUpdate
    {
        private static int MELEE_TRIGGER = Animator.StringToHash("MeleeAttack");
        private static int RANGE_TRIGGER = Animator.StringToHash("RangeAttack");

        private const string MELEE_ANIM_EVENT = "hit";
        private const string RANGE_ANIM_EVENT = "fire";

        [Get(MasterAPI.Animator)]
        public Animator animator;

        public AnimatorDispatcher dispatcher;

        [Get(MasterAPI.SpriteRenderer)]
        public SpriteRenderer spriteRenderer;

        private MoveAnimMechanics moveMechanics;

        private AnimationEventListener meleeAttackListener;
        private AnimationEventListener rangeAttackListener;

        public void Compose(CaptainEnemy_Core core)
        {
            this.moveMechanics = new MoveAnimMechanics(
                this.animator, core.moveComponent.IsMoving
            );

            this.meleeAttackListener = new AnimationEventListener(
                this.dispatcher, MELEE_ANIM_EVENT, core.meleeWeapon.FireAction
            );

            this.rangeAttackListener = new AnimationEventListener(
                this.dispatcher, RANGE_ANIM_EVENT, core.rangeWeapon.FireAction
            );

            core.switchToMeleeWeaponEvent.Subscribe(() =>
                this.animator.Play("SwitchToMelee", this.animator.GetLayerIndex("Extra Layer"), 0));
            core.switchToRangeWeaponEvent.Subscribe(() =>
                this.animator.Play("SwitchToRange", this.animator.GetLayerIndex("Extra Layer"), 0));

            core.attackComponent.AttackEvent.Subscribe(() =>
            {
                Weapon currentWeapon = core.currentWeapon.Value;
                if (currentWeapon == null)
                {
                    return;
                }

                if (!currentWeapon.FireCondition.Invoke())
                {
                    return;
                }

                if (currentWeapon is MeleeWeapon)
                {
                    this.animator.SetTrigger(MELEE_TRIGGER);
                }
                else if (currentWeapon is RangeWeapon)
                {
                    this.animator.SetTrigger(RANGE_TRIGGER);
                }
            });
        }

        public void Enable()
        {
            this.meleeAttackListener.Enable();
            this.rangeAttackListener.Enable();
        }

        public void Disable()
        {
            this.meleeAttackListener.Disable();
            this.rangeAttackListener.Disable();
        }

        public void OnUpdate(float deltaTime)
        {
            this.moveMechanics.Update();
        }
    }

    [Serializable]
    public sealed class CaptainEnemy_AI : IAtomicFixedUpdate, IAtomicEnable, IAtomicDisable
    {
        [SerializeField, HideInPlayMode]
        private bool aiEnabled = true;

        [Space]
        public Blackboard blackboard;
        public AIBehaviour behaviour;

        public void Compose(AtomicObject character)
        {
            this.blackboard.SetObject(BlackboardAPI.Character, character);
            character.AddField(nameof(Blackboard), this.blackboard);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (this.aiEnabled)
            {
                this.behaviour.OnUpdate(deltaTime);
            }
        }

        public void OnDrawGizmos()
        {
            if (this.aiEnabled)
            {
                this.behaviour.OnGizmos();
            }
        }

        public void Enable()
        {
            if (this.aiEnabled)
            {
                this.behaviour.OnStart();
            }
        }

        public void Disable()
        {
            if (this.aiEnabled)
            {
                this.behaviour.OnStop();
            }
        }
    }
}