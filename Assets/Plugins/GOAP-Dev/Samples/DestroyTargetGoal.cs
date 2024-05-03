using AIModule;
using Atomic;
using Game.Engine;
using UnityEngine;

namespace GOAPModule.Samples
{
    [CreateAssetMenu(
        fileName = "DestroyTargetGoal",
        menuName = "GOAP/Samples/New DestroyTargetGoal"
    )]
    public sealed class DestroyTargetGoal : GoapGoal
    {
        [SerializeField, BlackboardKey, Space]
        private ushort target;

        [SerializeField, Space]
        private int basePriority = 10;

        [SerializeField]
        private int extraPriority = 5;

        protected internal override bool IsValid(IBlackboard blackboard)
        {
            return blackboard.HasKey(this.target);
        }

        protected internal override int GetPriority(IBlackboard blackboard)
        {
            int priority = this.basePriority;
            
            IAtomicEntity target = blackboard.GetObject<IAtomicEntity>(this.target);

            if (target.TryGet(HealthAPI.Health, out HitPoints hitPoints))
            {
                priority += Mathf.RoundToInt(this.extraPriority * (1 - hitPoints.Percent));
            }
            
            return priority;
        }
    }
}