using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Sample
{
    [Serializable]
    public class TemporaryEffect : ICompletableEffect, IObject.IUpdate
    {
        [SerializeField]
        private AtomicCountdown countdown;

        private Action<IEffect> callback;
        
        public virtual void Apply(IObject obj)
        {
            this.countdown.Reset();
            this.countdown.Start();
            obj.AddLogic(this);
        }

        public virtual void Discard(IObject obj)
        {
            this.countdown.Stop();
            obj.DelLogic(this);
        }

        void ICompletableEffect.SetCallback(Action<IEffect> callback)
        {
            this.callback = callback;
        }
        
        public void OnUpdate(IObject obj, float deltaTime)
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