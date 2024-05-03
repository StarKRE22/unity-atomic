using Atomic;
using UnityEngine;

namespace Game.Engine
{
    public sealed class AttackAnimMechanics : IAtomicEnable, IAtomicDisable
    {
        private static readonly int Attack = Animator.StringToHash("Attack");

        private readonly Animator animator;
        private readonly IAtomicObservable attackEvent;
        private readonly int attackHash;

        public AttackAnimMechanics(Animator animator, IAtomicObservable attackEvent)
        {
            this.animator = animator;
            this.attackEvent = attackEvent;
            this.attackHash = Attack;
        }
        
        public AttackAnimMechanics(Animator animator, IAtomicObservable attackEvent, int attackHash)
        {
            this.animator = animator;
            this.attackEvent = attackEvent;
            this.attackHash = attackHash;
        }

        public void Enable()
        {
            this.attackEvent.Subscribe(this.OnAttack);
        }

        public void Disable()
        {
            this.attackEvent.Unsubscribe(this.OnAttack);
        }

        private void OnAttack()
        {
            this.animator.SetTrigger(this.attackHash);
        }
    }
}