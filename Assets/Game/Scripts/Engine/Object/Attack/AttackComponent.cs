using System;
using Atomic;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class AttackComponent : IDisposable
    {
        [Get(AttackAPI.AttackRequest)]
        public IAtomicAction AttackRequest => this.attackRequest; 
       
        public IAtomicObservable AttackEvent => this.attackEvent;
       
        public IAtomicExpression<bool> AttackCondition => this.attackCondition;

        [SerializeField]
        private AtomicAction attackRequest;
        
        [SerializeField]
        private AtomicEvent attackEvent;

        [SerializeField]
        private AtomicAnd attackCondition;
        
        public void Compose()
        {
            this.attackRequest.Compose(() =>
            {
                if (this.attackCondition.Invoke())
                {
                    this.attackEvent?.Invoke();
                }
            });
        }

        public void Dispose()
        {
            this.attackEvent?.Dispose();
        }
    }
}