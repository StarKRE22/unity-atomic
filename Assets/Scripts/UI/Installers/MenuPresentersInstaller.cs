using System;
using Modules.Contexts;

namespace SampleGame.UI
{
    [Serializable]
    public sealed class MenuPresentersInstaller : IContextInstaller
    {
        private MenuScreenPresenter
        
        public void Install(IContext context)
        {
            context.AddSystem<>()
        }
    }
}