using UnityEngine;
// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Atomic
{
    [AddComponentMenu("Atomic/Atomic Entity Installer")]
    public class AtomicEntityInstaller : MonoBehaviour
    {
        [SerializeField]
        private AtomicEntity target;
        
        [SerializeReference]
        private AtomicEntity.IInstaller[] installers = default;

        private void Awake()
        {
            this.Install();
        }

        private void Reset()
        {
            this.target = this.GetComponentInParent<AtomicEntity>();
        }
        
        [ContextMenu("Install")]
        public virtual void Install()
        {
            if (this.target == null)
            {
                return;
            }
            
            if (installers == null || this.installers.Length == 0)
            {
                return;
            }
            
            for (int i = 0, count = this.installers.Length; i < count; i++)
            {
                AtomicEntity.IInstaller installer = this.installers[i];
                installer.Install(this.target);
            }
        }
    }
}