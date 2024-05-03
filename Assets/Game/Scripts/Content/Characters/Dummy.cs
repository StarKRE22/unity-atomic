using Atomic;
using Game.Engine;
using UnityEngine;

namespace Game.Content
{
    [Is(TypeAPI.Damagable)]
    public sealed class Dummy : AtomicObject
    {
        #region API

        [Get(MasterAPI.Transform)]
        public Transform Transform => this.transform;

        [Get(HealthAPI.IsAlive)]
        public IAtomicValue<bool> IsAlive => this.healthComponent.IsAlive;

        [Get(HealthAPI.TakeDamageAction)]
        public IAtomicAction<int> TakeDamageAction => this.healthComponent.TakeDamageAction;

        #endregion

        #region CORE

        [SerializeField]
        private new Transform transform;

        [SerializeField]
        private HealthComponent healthComponent;

        [SerializeField]
        public new Collider2D collider;

        [SerializeField]
        private Animator animator;

        private void Awake()
        {
            this.InitHealthComponent();
        }

        private void InitHealthComponent()
        {
            this.healthComponent.Let(it =>
            {
                it.Compose();
                it.DeathEvent.Subscribe(() =>
                {
                    this.collider.enabled = false;
                    this.animator.Play("Destroy", -1, 0);
                });
                it.TakeDamageEvent.Subscribe(_ =>
                {
                    if (this.healthComponent.IsAlive.Value)
                    {
                        this.animator.Play("TakeDamage", -1, -0);
                    }
                });
            });
        }

        private void OnEnable()
        {
            this.healthComponent.OnEnable();
        }

        private void OnDisable()
        {
            this.healthComponent.OnDisable();
        }

        #endregion
    }
}