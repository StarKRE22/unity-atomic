using Atomic;
using UnityEngine;

namespace Game.Engine
{
    [CreateAssetMenu(
        fileName = "SpriteColorEffect",
        menuName = "Engine/Effects/New SpriteColorEffect"
    )]
    public sealed class SpriteColorEffect : ScriptableEffect
    {
        [SerializeField]
        private Color color;

        public override void Apply(IAtomicObject obj)
        {
            obj.Get<SpriteRenderer>(MasterAPI.SpriteRenderer).color = this.color;
        }

        public override void Discard(IAtomicObject obj)
        {
            obj.Get<SpriteRenderer>(MasterAPI.SpriteRenderer).color = Color.white;
        }
    }
}