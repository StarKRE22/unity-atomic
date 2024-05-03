using System;
using Atomic;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public class TemporaryEffect : ICompletableEffect, IAtomicFixedUpdate
    {
        [SerializeField]
        private Countdown countdown;

        private Action<IEffect> callback;
        
        public virtual void Apply(IAtomicObject obj)
        {
            obj.AddLogic(this);
        }

        public virtual void Discard(IAtomicObject obj)
        {
            obj.RemoveLogic(this);
        }

        void ICompletableEffect.SetCallback(Action<IEffect> callback)
        {
            this.callback = callback;
        }

        void IAtomicFixedUpdate.OnFixedUpdate(float deltaTime)
        {
            this.countdown.Tick(deltaTime);

            if (this.countdown.time <= 0)
            {
                this.callback?.Invoke(this);
            }
        }
    }
}