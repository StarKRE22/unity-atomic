using System;
using Atomic;
using Sirenix.OdinInspector;

namespace Game.Engine
{
    [Serializable]
    public sealed class DealDamageAction : IAtomicAction<IAtomicObject>
    {
        private IAtomicValue<int> damage;

        public void Compose(IAtomicValue<int> damage)
        {
            this.damage = damage;
        }

        [Button]
        public void Invoke(IAtomicObject target)
        {
            if (target.Is(TypeAPI.Damagable))
            {
                target.InvokeAction(HealthAPI.TakeDamageAction, this.damage.Value);
            }
        }
    }
}