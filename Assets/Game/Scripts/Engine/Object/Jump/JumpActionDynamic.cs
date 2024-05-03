using System;
using Atomic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class JumpActionDynamic : IAtomicAction
    {
        private readonly IAtomicObject atomicObject;

        public JumpActionDynamic(IAtomicObject atomicObject)
        {
            this.atomicObject = atomicObject;
        }

        [Button]
        public void Invoke()
        {
            if (!this.atomicObject.TryGet(MasterAPI.Rigidbody, out Rigidbody2D rigidbody) ||
                !this.atomicObject.TryGet(JumpAPI.JumpEnabled, out IAtomicValue<bool> enabled) ||
                !this.atomicObject.TryGet(JumpAPI.BaseJumpForce, out IAtomicValue<float> force))
            {
                return;
            }

            if (!enabled.Value)
            {
                return;
            }

            rigidbody.AddForce(new Vector2(0, force.Value), ForceMode2D.Impulse);
            this.atomicObject.InvokeAction(JumpAPI.JumpEvent);
        }
    }
}