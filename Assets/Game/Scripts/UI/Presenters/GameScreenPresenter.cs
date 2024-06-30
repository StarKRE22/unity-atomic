using System;
using Atomic.UI;
using Modules.Gameplay;

namespace SampleGame.UI
{
    [Serializable]
    public sealed class GameScreenPresenter : IInitBehaviour, IShowBehaviour, IHideBehaviour
    {
        private GameCycle gameCycle;
        
        private IView finishPopup;
        private IView startPopup;
        private IView pausePopup;

        public void Init(IView view)
        {
            this.gameCycle = GameContext.Instance.GetGameCycle();

            this.startPopup = view.GetStartPopup();
            this.startPopup.Show();
            
            this.finishPopup = view.GetFinishPopup();
            this.finishPopup.Hide();

            this.pausePopup = view.GetPausePopup();
            this.pausePopup.Hide();
        }

        public void Show(IView view)
        {
            this.gameCycle.OnStarted += this.startPopup.Hide;
            this.gameCycle.OnFinished += this.finishPopup.Show;
            this.gameCycle.OnPaused += this.pausePopup.Show;
            this.gameCycle.OnResumed += this.pausePopup.Hide;
        }

        public void Hide(IView view)
        {
            this.gameCycle.OnStarted -= this.startPopup.Hide;
            this.gameCycle.OnFinished -= this.finishPopup.Show;
            this.gameCycle.OnPaused -= this.pausePopup.Show;
            this.gameCycle.OnResumed -= this.pausePopup.Hide;
        }
    }
}