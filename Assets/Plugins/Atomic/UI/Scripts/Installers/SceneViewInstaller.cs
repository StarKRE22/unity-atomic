using UnityEngine;

namespace Atomic.UI
{
    public sealed class SceneViewInstaller : MonoBehaviour, IViewInstaller
    {
        [SerializeReference]
        private IHandler[] handlers;

        [SerializeReference]
        private IViewInstaller[] installers;
        
        public void Install(IView view)
        {
            if (this.installers != null)
            {
                
            }
        }
    }
}