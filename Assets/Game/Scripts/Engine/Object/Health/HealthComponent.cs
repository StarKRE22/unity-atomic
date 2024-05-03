using System;
using Atomic;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public class HealthComponent : IDisposable
    {
        public IAtomicObservable DeathEvent => this.deathEvent;
        public IAtomicAction<int> TakeDamageAction => this.takeDamageAction;
        public IAtomicObservable<int> TakeDamageEvent => this.takeDamageEvent;
        public IAtomicValue<bool> IsAlive => this.isAlive;

        [SerializeField]
        private HitPoints hitPoints = new(5, 5);

        [SerializeField]
        private AtomicFunction<bool> isAlive;

        private readonly AtomicEvent deathEvent = new();

        [SerializeField]
        private TakeDamageAction takeDamageAction = new();

        [SerializeField]
        private AtomicEvent<int> takeDamageEvent;

        private DeathMechanics deathMechanics;

        public void Compose()
        {
            takeDamageAction.Compose(hitPoints, this.takeDamageEvent);
            deathMechanics = new DeathMechanics(hitPoints, deathEvent);
            isAlive.Compose(() => this.hitPoints.Current > 0);
        }

        public void OnEnable()
        {
            deathMechanics.Enable();
        }

        public void OnDisable()
        {
            deathMechanics.Disable();
        }

        public void Dispose()
        {
            deathEvent?.Dispose();
        }
    }
}