using UnityEngine;

namespace Atomic.Objects
{
    [AddComponentMenu("Atomic/Mono Composer")]
    [DisallowMultipleComponent]
    public sealed class MonoComposer : MonoBehaviour, IAspect
    {
        [SerializeReference]
        private IAspect[] aspects;
        
        public void Compose(IObject obj)
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

        public void Dispose(IObject obj)
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