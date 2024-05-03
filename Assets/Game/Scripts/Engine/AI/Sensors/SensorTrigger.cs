using AIModule;
using Atomic;
using UnityEngine;

namespace Game.Engine
{
    public sealed class SensorTrigger : MonoBehaviour
    {
        [SerializeField]
        private AtomicObject enemy;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out IAtomicObject obj) && obj.Is(TypeAPI.Character))
            {
                this.enemy.Get<Blackboard>(nameof(Blackboard)).SetObject(BlackboardAPI.Target, obj);
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.TryGetComponent(out IAtomicObject obj) && obj.Is(TypeAPI.Character))
            {
                this.enemy.Get<Blackboard>(nameof(Blackboard)).DeleteObject(BlackboardAPI.Target);
            }
        }
    }
}