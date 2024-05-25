using UnityEngine;

namespace Atomic.Objects
{
    [CreateAssetMenu(
        fileName = "ScriptableSection",
        menuName = "Atomic/Objects/New ScriptableSection"
    )]
    public sealed class ScriptableSection : ScriptableObject, IAtomicAspect
    {
        [SerializeReference]
        private IAtomicAspect[] aspects;
        
        public void Compose(IAtomicObject obj)
        {
            if (this.aspects is {Length: > 0})
            {
                for (int i = 0, count = this.aspects.Length; i < count; i++)
                {
                    IAtomicAspect aspect = this.aspects[i];
                    if (aspect != null)
                    {
                        aspect.Compose(obj);
                    }
                }
            }
        }

        public void Dispose(IAtomicObject obj)
        {
            if (this.aspects is {Length: > 0})
            {
                for (int i = 0, count = this.aspects.Length; i < count; i++)
                {
                    IAtomicAspect aspect = this.aspects[i];
                    if (aspect != null)
                    {
                        aspect.Dispose(obj);
                    }
                }
            }
        }
    }
}