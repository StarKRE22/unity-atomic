using Atomic;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Sample
{
    public sealed class KillCharacterMechanics
    {
        public void OnCollisionEnter2D(Collision2D collision2D)
        {
            if (collision2D.TryGetAtomicObject(out var obj) && obj.HasCharacterTag())
            {
                obj.InvokeAtomicAction(CommonAPI.DeathAction);
            }
        }
    }
}