using UnityEngine;

namespace Atomic.Objects
{
    [CreateAssetMenu(
        fileName = "ScriptableComposer",
        menuName = "Atomic/Objects/New ScriptableComposer"
    )]
    public sealed class ScriptableComposer : ScriptableObject, IAspect
    {
        [SerializeReference]
        private IAspect[] aspects;
        
        public void Compose(IAtomicObject obj)
        {
            if (this.aspects is {Length: > 0})
            {
                for (int i = 0, count = this.aspects.Length; i < count; i++)
                {
                    var installer = this.aspects[i];
                    if (installer != null)
                    {
                        installer.Compose(obj);
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
                    var installer = this.aspects[i];
                    if (installer != null)
                    {
                        installer.Dispose(obj);
                    }
                }
            }
        }
    }
}