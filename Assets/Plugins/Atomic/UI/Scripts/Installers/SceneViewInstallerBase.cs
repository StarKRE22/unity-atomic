using UnityEngine;

namespace Atomic.UI
{
    public abstract class SceneViewInstallerBase : MonoBehaviour, IViewInstaller
    {
        public abstract void Install(IView view);
    }
}