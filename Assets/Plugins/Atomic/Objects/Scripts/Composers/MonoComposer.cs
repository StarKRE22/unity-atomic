using UnityEngine;

namespace Atomic.Objects
{
    [AddComponentMenu("Atomic/Mono Composer")]
    [DisallowMultipleComponent]
    public sealed class MonoComposer : MonoBehaviour, IComposable
    {
        [SerializeReference]
        private IComposable[] composables;
        
        public void Compose(IObject obj)
        {
            if (this.composables is {Length: > 0})
            {
                for (int i = 0, count = this.composables.Length; i < count; i++)
                {
                    var installer = this.composables[i];
                    if (installer != null)
                    {
                        installer.Compose(obj);
                    }
                }
            }
        }
    }
}