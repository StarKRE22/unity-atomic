using System;
using Atomic.UI;
using Modules.GameCycles;

namespace SampleGame.UI
{
    [Serializable]
    public sealed class GameScreenPresenter : IAwakeHandler, IEnableHandler, IDisableHandler
    {
        private GameCycle gameCycle;
        
        private IView finishPopup;
        private IView startPopup;
        private IView pausePopup;

        public void Awake(IView view)
        {
            this.gameCycle = GameContext.Instance.GetGameCycle();

            this.startPopup = view.GetStartPopup();
            this.startPopup.Show();
            
            this.finishPopup = view.GetFinishPopup();
            this.finishPopup.Hide();

            this.pausePopup = view.GetPausePopup();
            this.pausePopup.Hide();
        }

        public void Enable(IView view)
        {
            this.gameCycle.OnGameStarted += this.startPopup.Hide;
            this.gameCycle.OnGameFinished += this.finishPopup.Show;
            this.gameCycle.OnGamePaused += this.pausePopup.Show;
            this.gameCycle.OnGameResumed += this.pausePopup.Hide;
        }

        public void Disable(IView view)
        {
            this.gameCycle.OnGameStarted -= this.startPopup.Hide;
            this.gameCycle.OnGameFinished -= this.finishPopup.Show;
            this.gameCycle.OnGamePaused -= this.pausePopup.Show;
            this.gameCycle.OnGameResumed -= this.pausePopup.Hide;
        }
    }
}