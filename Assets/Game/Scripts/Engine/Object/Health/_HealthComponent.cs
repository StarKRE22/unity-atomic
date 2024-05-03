using System;
using Atomic;
using Atomic.Installers;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class _HealthComponent : AtomicObject.IInstaller
    {
        [SerializeField]
        private _Field<HitPoints> hitPoints = new(new HitPoints(5, 5));
        
        [SerializeField]
        private _Field_Id<TakeDamageAction> takeDamageAction;

        [SerializeField]
        private _Field_Id<AtomicEvent<int>> takeDamageEvent;

        [SerializeField]
        private _Field_Id<AtomicEvent> deathEvent;

        public void Install(AtomicObject obj)
        {
            var takeDamageEvent = new AtomicEvent<int>();
            var takeDamageAction = new TakeDamageAction(this.hitPoints.Value, takeDamageEvent);
            var deathEvent = new AtomicEvent();
            var deathMechanics = new DeathMechanics(this.hitPoints.Value, deathEvent);
            
            this.hitPoints.Install(obj);
            this.takeDamageEvent.Install(obj, takeDamageEvent);
            this.takeDamageAction.Install(obj, takeDamageAction);
            this.deathEvent.Install(obj, deathEvent);
            
            obj.AddLogic(deathMechanics);
        }
    }
}