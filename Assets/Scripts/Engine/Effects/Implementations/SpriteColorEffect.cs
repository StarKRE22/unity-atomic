using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Sample
{
    [CreateAssetMenu(
        fileName = "SpriteColorEffect",
        menuName = "Content/Effect/New SpriteColorEffect"
    )]
    public sealed class SpriteColorEffect : ScriptableEffect
    {
        [SerializeField]
        private Color color;

        public override void Apply(IObject obj)
        {
            SpriteRenderer spriteRenderer = obj.GetSpriteRenderer();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = this.color;
            }
        }

        public override void Discard(IObject obj)
        {
            SpriteRenderer spriteRenderer = obj.GetSpriteRenderer();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.white;
            }
        }
    }
}