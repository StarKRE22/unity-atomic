using Atomic.Elements;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Sample
{
    [CreateAssetMenu(
        fileName = "JumpAbilityEffect",
        menuName = "Content/Effect/New JumpAbilityEffect"
    )]
    public sealed class JumpAbilityEffect : ScriptableEffect
    {
        [SerializeField]
        private float baseForce = 10;
        
        public override void Apply(IObject obj)
        {
            if (obj.TryGetRigidbody2D(out var rigidbody))
            {
                obj.AddJumpComponent(new JumpComponent(rigidbody, baseForce));
            }
        }

        public override void Discard(IObject obj)
        {
            obj.DelJumpComponent();
        }
    }
}