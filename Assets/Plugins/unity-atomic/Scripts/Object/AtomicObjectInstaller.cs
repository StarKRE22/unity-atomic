using UnityEngine;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Atomic
{
    [AddComponentMenu("Atomic/Atomic Object Installer")]
    public class AtomicObjectInstaller : MonoBehaviour
    {
        [SerializeField]
        private AtomicObject target;

        [SerializeReference]
        private AtomicObject.IInstaller[] installers = default;

        private void Awake()
        {
            this.Install();
        }

        private void Reset()
        {
            this.target = this.GetComponentInParent<AtomicObject>();
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
                AtomicObject.IInstaller installer = this.installers[i];
                installer.Install(this.target);
            }
        }
    }
}