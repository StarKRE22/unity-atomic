using System;
using Atomic.Elements;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Sample
{
    [Serializable]
    public sealed class MoveAnimMechanics
    {
        private Animator animator;
        private IAtomicValue<bool> isMoving;
        private int isMovingHash; 

        public MoveAnimMechanics(Animator animator, IAtomicValue<bool> isMoving, int isMovingHash)
        {
            this.animator = animator;
            this.isMoving = isMoving;
            this.isMovingHash = isMovingHash;
        }

        public MoveAnimMechanics()
        {
        }

        public void Compose(Animator animator, IAtomicValue<bool> isMoving, int isMovingHash)
        {
            this.animator = animator;
            this.isMoving = isMoving;
            this.isMovingHash = isMovingHash;
        }

        public void Update()
        {
            this.animator.SetBool(this.isMovingHash, this.isMoving.Value);
        }
    }
}