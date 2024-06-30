using System;
using Atomic.UI;
using Modules.Gameplay;
using UnityEngine;

namespace SampleGame.UI
{
    [Serializable]
    public sealed class GameScreenPresenter : IInitBehaviour, IShowBehaviour, IHideBehaviour
    {
        [SerializeField]
        private View startPopup;
        
        [SerializeField]
        private View pausePopup;
        
        [SerializeField]
        private View finishPopup;

        private GameCycle gameCycle;

        public void Init(IView view)
        {
            this.gameCycle = GameContext.Instance.GetGameCycle();

            this.startPopup.Show();
            this.finishPopup.Hide();
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