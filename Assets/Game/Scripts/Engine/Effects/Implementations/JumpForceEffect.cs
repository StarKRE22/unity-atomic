using Atomic;
using UnityEngine;

namespace Game.Engine
{
    [CreateAssetMenu(
        fileName = "JumpForceEffect",
        menuName = "Engine/Effects/New JumpForceEffect"
    )]
    public sealed class JumpForceEffect : ScriptableEffect
    {
        [SerializeField]
        private AtomicValue<float> range;

        public override void Apply(IAtomicObject obj)
        {
            if (obj.TryGet(JumpAPI.FullJumpForce, out IAtomicExpression<float> forceExpression))
            {
                forceExpression.Append(this.range);
            }
        }

        public override void Discard(IAtomicObject obj)
        {
            if (obj.TryGet(JumpAPI.FullJumpForce, out IAtomicExpression<float> forceExpression))
            {
                forceExpression.Remove(this.range);
            }
        }
    }
}