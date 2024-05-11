using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Sample
{
    [Serializable]
    public class TemporaryEffect : ICompletableEffect, IAtomicObject.IUpdate
    {
        [SerializeField]
        private AtomicCountdown countdown;

        private Action<IEffect> callback;
        
        public virtual void Apply(IAtomicObject obj)
        {
            this.countdown.Reset();
            this.countdown.Start();
            obj.AddBehaviour(this);
        }

        public virtual void Discard(IAtomicObject obj)
        {
            this.countdown.Stop();
            obj.DelBehaviour(this);
        }

        void ICompletableEffect.SetCallback(Action<IEffect> callback)
        {
            this.callback = callback;
        }
        
        public void OnUpdate(IAtomicObject obj, float deltaTime)
        {
            if (!this.countdown.IsPlaying)
            {
                return;
            }

            this.countdown.Tick(deltaTime);

            if (!this.countdown.IsPlaying)
            {
                this.callback?.Invoke(this);
            }
        }
    }
}