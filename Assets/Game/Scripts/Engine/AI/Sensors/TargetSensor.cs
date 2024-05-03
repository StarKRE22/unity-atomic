using AIModule;
using Atomic;
using UnityEditor;
using UnityEngine;

namespace Game.Engine
{
    [CreateAssetMenu(
        fileName = "TargetSensor",
        menuName = "Engine/AI/New TargetSensor"
    )]
    public sealed class TargetSensor : AIMechanics, IAIGizmos
    {
        private static readonly Collider2D[] buffer = new Collider2D[32];

        [SerializeField, BlackboardKey]
        private ushort center;

        [SerializeField, BlackboardKey]
        private ushort boxSize;

        [SerializeField]
        private LayerMask layerMask;

        public override void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetObject(this.center, out Transform center) ||
                !blackboard.TryGetVector2(this.boxSize, out Vector2 boxSize))
            {
                return;
            }

            int count = Physics2D.OverlapBoxNonAlloc(center.position, boxSize, 0, buffer, this.layerMask);
            for (var i = 0; i < count; i++)
            {
                Collider2D collider = buffer[i];

                if (!collider.TryGetComponent(out IAtomicObject obj))
                {
                    continue;
                }

                if (obj.TryGet(HealthAPI.IsAlive, out IAtomicValue<bool> isAlive) && isAlive.Value)
                {
                    blackboard.SetObject(BlackboardAPI.Target, obj);
                    return;
                }
            }

            blackboard.DeleteObject(BlackboardAPI.Target);
        }

        public void OnGizmos(IBlackboard blackboard)
        {
            if (!blackboard.TryGetObject(this.center, out Transform center) ||
                !blackboard.TryGetVector2(this.boxSize, out Vector2 boxSize))
            {
                return;
            }

            Handles.color = Color.magenta;
            Handles.DrawWireCube(center.position, boxSize);
        }
    }
}

