using UnityEngine;

namespace Atomic.Objects
{
    [CreateAssetMenu(
        fileName = "ScriptableComposer",
        menuName = "Atomic/Objects/New ScriptableComposer"
    )]
    public sealed class ScriptableComposer : ScriptableObject, IComposable
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