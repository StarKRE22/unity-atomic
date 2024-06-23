using Atomic.Objects;
using UnityEngine;

namespace Atomic.Extensions
{
    [CreateAssetMenu(
        fileName = "Scriptable Aspect",
        menuName = "MonoObjects/New ScriptableAspect"
    )]
    public sealed class ScriptableAspect : ScriptableAspectBase
    {
        [SerializeReference]
        private IAspect[] aspects;
        
        public override void Apply(IObject obj)
        {
            if (this.aspects != null)
            {
                for (int i = 0, count = this.aspects.Length; i < count; i++)
                {
                    this.aspects[i].Apply(obj);
                }
            }
        }

        public override void Discard(IObject obj)
        {
            if (this.aspects != null)
            {
                for (int i = 0, count = this.aspects.Length; i < count; i++)
                {
                    this.aspects[i].Discard(obj);
                }
            }
        }
    }
}