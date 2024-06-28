using System;
using Modules.Contexts;

namespace SampleGame.App
{
    [Serializable]
    public sealed class ApplicationInstaller : IContextInstaller
    {
        public void Install(IContext context)
        {
            context.AddSystem<ApplicationExitSystem>();
        }
    }
}