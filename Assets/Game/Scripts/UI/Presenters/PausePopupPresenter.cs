using System;
using Atomic.UI;
using Modules.Gameplay;
using SampleGame.App;

namespace SampleGame.UI
{
    [Serializable]
    public sealed class PausePopupPresenter : IShowViewBehaviour, IHideViewBehaviour
    {
        private GameCycle gameCycle;

        public void Show(IView view)
        {
            this.gameCycle = GameContext.Instance.GetGameCycle();

            view.GetResumeButton().onClick.AddListener(this.gameCycle.Resume);
            view.GetFinishButton().onClick.AddListener(this.gameCycle.Finish);
            view.GetExitButton().onClick.AddListener(ApplicationCase.Exit);
        }

        public void Hide(IView view)
        {
            view.GetResumeButton().onClick.RemoveListener(this.gameCycle.Resume);
            view.GetFinishButton().onClick.RemoveListener(this.gameCycle.Finish);
            view.GetExitButton().onClick.RemoveListener(ApplicationCase.Exit);
        }
    }
}