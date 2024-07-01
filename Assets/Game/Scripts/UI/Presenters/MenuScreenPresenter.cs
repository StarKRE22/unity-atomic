using System;
using Atomic.UI;
using SampleGame.App;

namespace SampleGame.UI
{
    [Serializable]
    public sealed class MenuScreenPresenter : IShowViewBehaviour, IHideViewBehaviour
    {
        public void Show(IView view)
        {
            // GameLauncher gameLauncher = ApplicationContext.Instance.GetGameLauncher();
            
            view.GetStartButton().onClick.AddListener(GameLaunchCase.LaunchGame);
            view.GetExitButton().onClick.AddListener(ApplicationCase.Exit);
        }

        public void Hide(IView view)
        {
            view.GetStartButton().onClick.RemoveListener(GameLaunchCase.LaunchGame);
            view.GetExitButton().onClick.RemoveListener(ApplicationCase.Exit);
        }
    }
}