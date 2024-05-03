using System.Collections.Generic;
using AIModule;
using Atomic;
using Game.Engine;
using UnityEngine;

namespace GOAPModule.Samples
{
    [CreateAssetMenu(
        fileName = "EnemySensor",
        menuName = "GOAP/Samples/New EnemySensor"
    )]
    public sealed class EnemySensor : GoapSensor
    {
        ///Blackboard:
        [SerializeField, BlackboardKey, Space]
        private ushort character;

        [SerializeField, BlackboardKey]
        private ushort enemy;

        [SerializeField, BlackboardKey]
        private ushort nearDistance;

        [SerializeField, BlackboardKey]
        private ushort atDistance;

        ///World State:
        [SerializeField, WorldStateKey, Space]
        private ushort hasEnemy;

        [SerializeField, WorldStateKey]
        private ushort nearEnemy;

        [SerializeField, WorldStateKey]
        private ushort atEnemy;

        protected internal override void GenerateFacts(IBlackboard blackboard, IDictionary<ushort, bool> worldState)
        {
            if (blackboard.TryGetObject(this.character, out IAtomicEntity character) &&
                blackboard.TryGetObject(this.enemy, out IAtomicEntity enemy) &&
                blackboard.TryGetFloat(this.nearDistance, out float nearDistance) &&
                blackboard.TryGetFloat(this.atDistance, out float atDistance))
            {
                Vector3 characterPosition = character.Get<Transform>(MasterAPI.Transform).position;
                Vector3 enemyPosition = enemy.Get<Transform>(MasterAPI.Transform).position;
                float currentDistance = (enemyPosition - characterPosition).sqrMagnitude;

                worldState[this.hasEnemy] = true;
                worldState[this.nearEnemy] = currentDistance <= nearDistance * nearDistance;
                worldState[this.atEnemy] = currentDistance <= atDistance * atDistance;
            }
        }
    }
}