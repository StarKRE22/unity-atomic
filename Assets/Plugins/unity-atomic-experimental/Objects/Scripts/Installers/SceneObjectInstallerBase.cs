using UnityEngine;

namespace Atomic.Objects
{
    public abstract class SceneObjectInstallerBase : MonoBehaviour, IObjectInstaller
    {
        public abstract void Install(IObject obj);
    }
}