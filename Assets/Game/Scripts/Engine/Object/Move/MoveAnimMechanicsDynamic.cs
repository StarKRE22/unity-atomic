using Atomic;
using UnityEngine;

namespace Game.Engine
{
    public sealed class MoveAnimMechanicsDynamic : IAtomicUpdate
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        private readonly IAtomicObject target;

        public MoveAnimMechanicsDynamic(IAtomicObject target)
        {
            this.target = target;
        }

        public void OnUpdate(float deltaTime)
        {
            if (!this.target.TryGet(MasterAPI.Animator, out Animator animator) || 
                !this.target.TryGetValue<bool>(MovementAPI.IsMoving, out var isMoving)) 
            {
                return;
            }
            
            animator.SetBool(IsMoving, isMoving.Value);
        }
    }
}