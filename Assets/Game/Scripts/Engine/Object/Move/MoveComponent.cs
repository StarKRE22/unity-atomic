using System;
using Atomic;
using UnityEngine;
using UnityEngine.Serialization;

// ReSharper disable MemberInitializerValueIgnored

namespace Game.Engine
{
    [Serializable]
    public sealed class MoveComponent : IDisposable, IAtomicFixedUpdate
    {
        public IAtomicVariableObservable<float> Direction => this.direction;
        public IAtomicExpression<float> SpeedExpression => this.fullSpeed;
        public IAtomicValue<bool> IsMoving => this.isMoving;
        public IAtomicValue<float> FullSpeed => this.fullSpeed;
        public IAtomicExpression<bool> Enabled => this.enabled;

        [SerializeField]
        private Rigidbody2D rigidbody;

        [SerializeField]
        private AtomicVariable<float> direction = new();

        [SerializeField]
        private AtomicAnd enabled = new();

        [SerializeField]
        private AtomicFunction<bool> isMoving = new();

        [SerializeField, FormerlySerializedAs("moveSpeed")]
        private AtomicValue<float> baseSpeed = new(0);

        [SerializeField]
        private AtomicFloatMul fullSpeed;

        private MovementMechanics movementMechanics;

        public void Compose()
        {
            this.fullSpeed.Append(this.baseSpeed);
            this.isMoving.Compose(() => this.fullSpeed.Invoke() != 0 &&
                                        this.direction.Value != 0 &&
                                        this.enabled.Invoke());
            
            this.movementMechanics = new MovementMechanics(
                this.rigidbody, this.enabled, this.direction, this.fullSpeed
            );
        }

        public void OnFixedUpdate(float deltaTime)
        {
            this.movementMechanics.FixedUpdate();
        }

        public void Dispose()
        {
            this.direction?.Dispose();
        }
    }
}