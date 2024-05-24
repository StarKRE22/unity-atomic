using Atomic.Elements;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Sample
{
    [CreateAssetMenu(
        fileName = "MoveSpeedEffect",
        menuName = "Content/Effect/New MoveSpeedEffect"
    )]
    public sealed class MoveSpeedEffect : ScriptableEffect
    {
        [SerializeField]
        private AtomicValue<float> multiplier;

        public override void Apply(IAtomicObject obj)
        {
            obj.GetMoveComponent()?.FullSpeed?.Append(this.multiplier);
        }

        public override void Discard(IAtomicObject obj)
        {
            obj.GetMoveComponent()?.FullSpeed?.Remove(this.multiplier);
        }
    }
}