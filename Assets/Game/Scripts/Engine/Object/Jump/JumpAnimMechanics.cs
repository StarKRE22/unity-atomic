using Atomic;
using UnityEngine;

namespace Game.Engine
{
    public sealed class JumpAnimMechanics
    {
        private static readonly int Jump = Animator.StringToHash("Jump");
        
        private readonly Animator animator;
        private readonly IAtomicObservable jumpEvent;

        public JumpAnimMechanics(Animator animator, IAtomicObservable jumpEvent)
        {
            this.animator = animator;
            this.jumpEvent = jumpEvent;
        }

        public void OnEnable()
        {
            this.jumpEvent.Subscribe(this.OnJump);
        }

        public void OnDisable()
        {
            this.jumpEvent.Unsubscribe(this.OnJump);
        }

        private void OnJump()
        {
            this.animator.SetTrigger(Jump);
        }
    }
}