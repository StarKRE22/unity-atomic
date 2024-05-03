using Atomic;
using UnityEngine;

namespace Game.Engine
{
    public sealed class MoveAnimMechanics
    {
        private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");

        private readonly Animator animator;
        private readonly IAtomicValue<bool> isMoving;
        private readonly int isMovingHash;

        public MoveAnimMechanics(Animator animator, IAtomicValue<bool> isMoving)
        {
            this.animator = animator;
            this.isMoving = isMoving;
            this.isMovingHash = IsMovingHash;
        }

        public MoveAnimMechanics(Animator animator, IAtomicValue<bool> isMoving, int isMovingHash)
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