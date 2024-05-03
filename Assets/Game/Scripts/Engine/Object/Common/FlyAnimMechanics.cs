using Atomic;
using UnityEngine;

namespace Game.Engine
{
    public sealed class FlyAnimMechanics
    {
        private static readonly int SpeedY = Animator.StringToHash("SpeedY");

        private readonly Animator animator;
        private readonly IAtomicValue<float> speedY;

        public FlyAnimMechanics(Animator animator, IAtomicValue<float> speedY)
        {
            this.animator = animator;
            this.speedY = speedY;
        }

        public void Update()
        {
            this.animator.SetFloat(SpeedY, this.speedY.Value);
        }
    }
}