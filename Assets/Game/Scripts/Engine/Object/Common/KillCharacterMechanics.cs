using Atomic;
using UnityEngine;

namespace Game.Engine
{
    public sealed class KillCharacterMechanics
    {
        public void OnCollisionEnter2D(Collision2D collision2D)
        {
            if (collision2D.gameObject.TryGetComponent(out IAtomicObject obj) && obj.Is(TypeAPI.Character))
            {
                obj.Get<IAtomicAction>(HealthAPI.DeathAction).Invoke();
            }
        }
    }
}