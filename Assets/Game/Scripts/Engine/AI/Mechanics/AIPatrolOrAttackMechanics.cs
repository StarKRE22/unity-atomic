using AIModule;
using UnityEngine;

namespace Game.Engine
{
    [CreateAssetMenu(
        fileName = "AIPirateBehaviour",
        menuName = "Engine/AI/New AIPirateBehaviour"
    )]
    public sealed class AIPatrolOrAttackMechanics : AIMechanics
    {
        [SerializeField, BlackboardKey]
        private ushort target;
        
        [SerializeField, BlackboardKey]
        private ushort attackEnabled;
        
        [SerializeField, BlackboardKey]
        private ushort patrolEnabled;

        public override void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            bool hasTarget = blackboard.HasKey(this.target);
            blackboard.SetBool(this.attackEnabled, hasTarget);
            blackboard.SetBool(this.patrolEnabled, !hasTarget);
        }
    }
}