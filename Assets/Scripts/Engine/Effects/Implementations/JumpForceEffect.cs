using Atomic.Elements;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Sample
{
    [CreateAssetMenu(
        fileName = "JumpForceEffect",
        menuName = "Content/Effect/New JumpForceEffect"
    )]
    public sealed class JumpForceEffect : ScriptableEffect
    {
        [SerializeField]
        private AtomicValue<float> range;

        public override void Apply(IObject obj)
        {
            obj.GetJumpComponent()?.FullForce?.Append(this.range);
        }

        public override void Discard(IObject obj)
        {
            obj.GetJumpComponent()?.FullForce?.Remove(this.range);
        }
    }
}