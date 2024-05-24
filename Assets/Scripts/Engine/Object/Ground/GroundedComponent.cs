using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;
using IDisposable = System.IDisposable;

namespace Sample
{
    [Serializable]
    public sealed class GroundedComponent : IObject.IFixedUpdate, IDisposable, IObject.IDrawGizmos
    {
        public Transform groundPoint;
        public AtomicVariable<bool> isGrounded;
        public AtomicValue<float> groundDistance = new(0.1f);

        public void OnFixedUpdate(IObject obj, float deltaTime)
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

        public void OnGizmosDraw(IObject obj)
        {
            Gizmos.DrawLine(this.groundPoint.position,
                this.groundPoint.position + Vector3.down * this.groundDistance.Value);
        }
#endif
    }
}