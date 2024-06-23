using UnityEngine;

namespace Atomic.Objects
{
    [AddComponentMenu("Mono Objects/Scene Object Installer")]
    public class SceneObjectInstaller : SceneObjectInstallerBase
    {
        [Header("Installers")]
        [SerializeReference]
        protected IObjectInstaller[] installers;
        
        [Header("Logics")]
        [SerializeReference]
        protected ILogic[] logics;

        public override void Install(IObject obj)
        {
            if (this.installers is {Length: > 0})
            {
                for (int i = 0, count = this.installers.Length; i < count; i++)
                {
                    IObjectInstaller installer = this.installers[i];
                    if (installer != null)
                    {
                        installer.Install(obj);
                    }
                }
            }

            if (this.logics is {Length: > 0})
            {
                for (int i = 0, count = this.logics.Length; i < count; i++)
                {
                    ILogic logic = this.logics[i];
                    if (logic != null)
                    {
                        obj.AddLogic(logic);
                    }
                }
            }
        }
    }
}