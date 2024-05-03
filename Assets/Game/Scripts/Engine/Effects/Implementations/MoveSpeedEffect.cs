using Atomic;
using UnityEngine;

namespace Game.Engine
{
    [CreateAssetMenu(
        fileName = "MoveSpeedEffect",
        menuName = "Engine/Effects/New MoveSpeedEffect"
    )]
    public sealed class MoveSpeedEffect : ScriptableEffect
    {
        [SerializeField]
        private AtomicValue<float> multiplier;

        public override void Apply(IAtomicObject obj)
        {
            if (obj.TryGet(MovementAPI.FullMoveSpeed, out IAtomicExpression<float> moveSpeedExpression))
            {
                moveSpeedExpression.Append(this.multiplier);
            }
        }

        public override void Discard(IAtomicObject obj)
        {
            if (obj.TryGet(MovementAPI.FullMoveSpeed, out IAtomicExpression<float> moveSpeedExpression))
            {
                moveSpeedExpression.Remove(this.multiplier);
            }
        }
    }
}