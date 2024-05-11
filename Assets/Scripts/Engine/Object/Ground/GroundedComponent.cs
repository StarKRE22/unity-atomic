using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Sample
{
    [Serializable]
    public sealed class GroundedComponent : IAtomicObject.IFixedUpdate, IDisposable, IAtomicObject.IDrawGizmos
    {
        public Transform groundPoint;
        public AtomicVariable<bool> isGrounded;
        public AtomicValue<float> groundDistance = new(0.1f);

        public void OnFixedUpdate(IAtomicObject obj, float deltaTime)
        {
            this.isGrounded.Value = Physics2D.Raycast(
                this.groundPoint.position, Vector2.down, this.groundDistance.Value, LayerMask.GetMask("Ground")
            );
        }

        public void Dispose()
        {
            this.isGrounded?.Dispose();
        }

#if UNITY_EDITOR

        public void OnGizmosDraw(IAtomicObject obj)
        {
            Gizmos.DrawLine(this.groundPoint.position,
                this.groundPoint.position + Vector3.down * this.groundDistance.Value);
        }
#endif
    }
}