using System;
using Atomic.UI;
using Modules.GameCycles;
using SampleGame.App;

namespace SampleGame.UI
{
    [Serializable]
    public sealed class PausePopupPresenter : IEnableHandler, IDisableHandler
    {
        private GameCycle gameCycle;

        public void Enable(IView view)
        {
            this.gameCycle = GameContext.Instance.GetGameCycle();

            view.GetResumeButton().onClick.AddListener(this.gameCycle.ResumeGame);
            view.GetFinishButton().onClick.AddListener(this.gameCycle.FinishGame);
            view.GetExitButton().onClick.AddListener(ApplicationCase.Exit);
        }

        public void Disable(IView view)
        {
            view.GetResumeButton().onClick.RemoveListener(this.gameCycle.ResumeGame);
            view.GetFinishButton().onClick.RemoveListener(this.gameCycle.FinishGame);
            view.GetExitButton().onClick.RemoveListener(ApplicationCase.Exit);
        }
    }
}