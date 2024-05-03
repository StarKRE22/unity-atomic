using Atomic;
using UnityEngine;

namespace Game.Engine
{
    [CreateAssetMenu(
        fileName = "JumpAbilityEffect",
        menuName = "Engine/Effects/New JumpAbilityEffect"
    )]
    public sealed class JumpAbilityEffect : ScriptableEffect
    {
        [SerializeField]
        private float force = 10;
        
        public override void Apply(IAtomicObject obj)
        {
            JumpAspect.Compose(obj, this.force);
        }

        public override void Discard(IAtomicObject obj)
        {
            JumpAspect.Dispose(obj);
        }
    }
}