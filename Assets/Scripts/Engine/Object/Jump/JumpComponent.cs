using System;
using Atomic.Elements;
using UnityEngine;

namespace Sample
{
    [Serializable]
    public sealed class JumpComponent : ISerializationCallbackReceiver, IDisposable
    {
        public IAtomicAction JumpAction => this.jumpAction;
        public IAtomicEvent JumpEvent => this.jumpEvent;
        public IAtomicExpression<bool> Enabled => this.enabled;
        public IAtomicValue<float> BaseForce => this.baseForce;
        public IAtomicExpression<float> FullForce => this.fullForce;

        [SerializeField]
        private AtomicAnd enabled;

        [SerializeField]
        private AtomicAction jumpAction;

        [SerializeField]
        private AtomicEvent jumpEvent;

        [SerializeField]
        private Rigidbody2D rigidbody;

        [SerializeField]
        private AtomicValue<float> baseForce;

        [SerializeField]
        private AtomicFloatMul fullForce;
        
        public JumpComponent()
        {
        }
        
        public JumpComponent(Rigidbody2D rigidbody, float baseForce)
        {
            this.rigidbody = rigidbody;
            this.baseForce = new AtomicValue<float>(baseForce);
            this.fullForce = new AtomicFloatMul(this.baseForce);
            this.enabled = new AtomicAnd();
            this.jumpEvent = new AtomicEvent();
            this.jumpAction = new AtomicAction(this.Jump);
        }

        public void OnAfterDeserialize()
        {
            this.jumpAction.Compose(this.Jump);
            this.fullForce.Append(this.baseForce);
        }

        public void OnBeforeSerialize()
        {
        }

        private void Jump()
        {
            Debug.Log("JUMP!");
            if (this.enabled.Invoke())
            {
                this.rigidbody.AddForce(new Vector2(0, this.FullForce.Value), ForceMode2D.Impulse);
                this.jumpEvent?.Invoke();
            }
        }

        public void Dispose()
        {
            this.jumpEvent?.Dispose();
        }
    }
}