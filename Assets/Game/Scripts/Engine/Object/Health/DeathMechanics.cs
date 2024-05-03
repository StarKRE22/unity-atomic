using System;
using Atomic;

namespace Game.Engine
{
    public sealed class DeathMechanics : IAtomicEnable, IAtomicDisable
    {
        private readonly HitPoints hitPoints;
        private readonly IAtomicEvent deathEvent;

        public DeathMechanics(HitPoints hitPoints, IAtomicEvent deathEvent)
        {
            this.hitPoints = hitPoints;
            this.deathEvent = deathEvent;
        }

        public void Enable()
        {
            hitPoints.OnChanged += OnHitPointsChanged;
        }

        public void Disable()
        {
            hitPoints.OnChanged -= OnHitPointsChanged;
        }

        private void OnHitPointsChanged(int hp)
        {
            if (hp <= 0)
                deathEvent?.Invoke();
        }
    }
}