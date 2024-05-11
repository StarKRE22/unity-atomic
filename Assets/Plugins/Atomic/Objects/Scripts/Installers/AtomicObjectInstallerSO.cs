using UnityEngine;

namespace Atomic.Objects
{
    [CreateAssetMenu(
        fileName = "AtomicObjectInstallerSO",
        menuName = "Atomic/Objects/New AtomicObjectInstallerSO"
    )]
    public class AtomicObjectInstallerSO : ScriptableObject, IAtomicObject.IComposable
    {
        [SerializeReference]
        private IAtomicObject.IComposable[] installers;
        
        public void Compose(IAtomicObject obj)
        {
            if (this.installers is {Length: > 0})
            {
                for (int i = 0, count = this.installers.Length; i < count; i++)
                {
                    var installer = this.installers[i];
                    if (installer != null)
                    {
                        installer.Compose(obj);
                    }
                }
            }
        }
    }
}