using System;
using Atomic.UI;
using SampleGame.App;

namespace SampleGame.UI
{
    [Serializable]
    public sealed class MenuScreenPresenter : IEnableHandler, IDisableHandler
    {
        public void Enable(IView view)
        {
            view.GetStartButton().onClick.AddListener(GameLaunchCase.LaunchGame);
            view.GetExitButton().onClick.AddListener(ApplicationCase.Exit);
        }

        public void Disable(IView view)
        {
            view.GetStartButton().onClick.RemoveListener(GameLaunchCase.LaunchGame);
            view.GetExitButton().onClick.RemoveListener(ApplicationCase.Exit);
        }
    }
}