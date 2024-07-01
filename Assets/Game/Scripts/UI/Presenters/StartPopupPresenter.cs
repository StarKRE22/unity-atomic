using System;
using Atomic.UI;
using Modules.Gameplay;

namespace SampleGame.UI
{
    [Serializable]
    public sealed class StartPopupPresenter : IShowViewBehaviour, IHideViewBehaviour
    {
        private GameCycle gameCycle;
        
        public void Show(IView view)
        {
            this.gameCycle = GameContext.Instance.GetGameCycle();
            view.GetStartButton().onClick.AddListener(this.gameCycle.Start);
        }

        public void Hide(IView view)
        {
            view.GetStartButton().onClick.RemoveListener(this.gameCycle.Start);
        }
    }
}