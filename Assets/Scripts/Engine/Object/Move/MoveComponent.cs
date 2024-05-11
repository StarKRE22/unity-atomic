using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Sample
{
    [Serializable]
    public sealed class MoveComponent : ISerializationCallbackReceiver, IAtomicObject.IFixedUpdate, IDisposable
    {
        public IAtomicValue<bool> IsMoving => this.isMoving;
        public IAtomicVariable<float> CurrentDirection => this.currentDirection;
        public IAtomicExpression<bool> Enabled => this.enabled;
        public IAtomicValue<float> BaseSpeed => this.baseSpeed;
        public IAtomicExpression<float> FullSpeed => this.fullSpeed;

        [SerializeField]
        private AtomicAnd enabled;

        [SerializeField]
        private AtomicVariable<float> currentDirection;

        [SerializeField]
        private AtomicFunction<bool> isMoving;

        [SerializeField]
        private AtomicValue<float> baseSpeed;

        [SerializeField]
        private AtomicFloatMul fullSpeed;

        [SerializeField]
        private Rigidbody2D rigidbody;

        public MoveComponent()
        {
        }

        public MoveComponent(Rigidbody2D rigidbody, float baseSpeed = 0)
        {
            this.enabled = new AtomicAnd();
            this.currentDirection = new AtomicVariable<float>();
            this.isMoving = new AtomicFunction<bool>(this.IsMove);
            this.baseSpeed = new AtomicValue<float>(baseSpeed);
            this.fullSpeed = new AtomicFloatMul(this.baseSpeed);
            this.rigidbody = rigidbody;
        }

        public void OnAfterDeserialize()
        {
            this.fullSpeed.Append(this.baseSpeed);
            this.isMoving = new AtomicFunction<bool>(this.IsMove);
        }

        public void OnBeforeSerialize()
        {
        }

        public void Dispose()
        {
            this.fullSpeed.Remove(this.baseSpeed);
            this.currentDirection?.Dispose();
        }

        public void OnFixedUpdate(IAtomicObject _, float deltaTime)
        {
            float speedX = 0.0f;
            float speedY = this.rigidbody.velocity.y;

            if (this.enabled.Invoke())
            {
                speedX = this.currentDirection.Value * this.fullSpeed.Invoke();
            }

            this.rigidbody.velocity = new Vector2(speedX, speedY);
        }

        private bool IsMove()
        {
            return this.fullSpeed.Invoke() != 0 &&
                   this.currentDirection.Value != 0 &&
                   this.enabled.Invoke();
        }
    }
}