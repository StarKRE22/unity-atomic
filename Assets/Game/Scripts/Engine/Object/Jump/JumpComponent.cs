using System;
using Atomic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Engine
{
    [Serializable]
    [Is(TypeAPI.Jumpable)]
    public sealed class JumpComponent
    {
        [Get(JumpAPI.JumpAction)]
        public IAtomicAction JumpAction => this.jumpAction;

        [Get(JumpAPI.FullJumpForce)]
        public IAtomicExpression<float> JumpForceExpression => this.fullForce;

        public IAtomicExpression<bool> JumpEnabled => this.jumpEnabled;

        public IAtomicObservable JumpEvent => this.jumpEvent;

        [SerializeField]
        private Rigidbody2D rigidbody;

        [SerializeField, FormerlySerializedAs("jumpForce")]
        private AtomicValue<float> baseForce;

        [SerializeField]
        private AtomicFloatSum fullForce;

        [SerializeField]
        private AtomicAnd jumpEnabled;

        [SerializeField]
        private JumpAction jumpAction;

        [SerializeField]
        private AtomicEvent jumpEvent;

        public void Compose()
        {
            this.jumpAction.Compose(
                this.rigidbody,
                this.fullForce,
                this.jumpEnabled,
                this.jumpEvent
            );
            
            this.fullForce.Append(this.baseForce);
        }
    }
}