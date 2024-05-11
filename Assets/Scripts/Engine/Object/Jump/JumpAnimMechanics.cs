using System;
using Atomic.Elements;
using UnityEngine;

namespace Sample
{
    [Serializable]
    public sealed class JumpAnimMechanics
    {
        private Animator animator;
        private IAtomicObservable jumpEvent;
        private int jumpHash;

        public JumpAnimMechanics()
        {
        }

        public JumpAnimMechanics(Animator animator, IAtomicObservable jumpEvent, int jumpHash)
        {
            this.animator = animator;
            this.jumpEvent = jumpEvent;
            this.jumpHash = jumpHash;
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
            this.animator.SetTrigger(this.jumpHash);
        }

        public void Compose(Animator animator, IAtomicObservable jumpEvent, int jumpHash)
        {
            this.animator = animator;
            this.jumpEvent = jumpEvent;
            this.jumpHash = jumpHash;
        }
    }
}