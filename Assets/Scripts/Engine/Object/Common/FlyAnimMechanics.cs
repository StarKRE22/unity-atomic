using System;
using Atomic.Elements;
using UnityEngine;

namespace Sample
{
    [Serializable]
    public sealed class FlyAnimMechanics
    {
        private static readonly int SpeedY = Animator.StringToHash("SpeedY");

        private Animator animator;
        private IAtomicValue<float> speedY;

        public FlyAnimMechanics()
        {
        }

        public FlyAnimMechanics(Animator animator, IAtomicValue<float> speedY)
        {
            this.animator = animator;
            this.speedY = speedY;
        }

        public void Update()
        {
            this.animator.SetFloat(SpeedY, this.speedY.Value);
        }

        public void Compose(Animator animator, IAtomicValue<float> speedY)
        {
            this.animator = animator;
            this.speedY = speedY;
        }
    }
}