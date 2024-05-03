using System;
using Atomic;

namespace Game.Engine
{
    [Serializable]
    public sealed class AnimationEventListener : IAtomicEnable, IAtomicDisable
    {
        private readonly AnimatorDispatcher animator;
        private readonly IAtomicAction action;
        private readonly string animationEvent;

        public AnimationEventListener(AnimatorDispatcher animator, string animationEvent, IAtomicAction action)
        {
            this.animator = animator;
            this.animationEvent = animationEvent;
            this.action = action;
        }

        public void Enable()
        {
            this.animator.AddListener(this.animationEvent, this.action.Invoke); 
        }

        public void Disable()
        {
            this.animator.RemoveListener(this.animationEvent, this.action.Invoke);
        }
    }
}