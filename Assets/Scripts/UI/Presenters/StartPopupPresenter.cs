using System;
using Atomic.UI;
using Modules.GameCycles;

namespace SampleGame.UI
{
    [Serializable]
    public sealed class StartPopupPresenter : IEnableHandler, IDisableHandler
    {
        private GameCycle gameCycle;
        
        public void Enable(IView view)
        {
            this.gameCycle = GameContext.Instance.GetGameCycle();
            view.GetStartButton().onClick.AddListener(this.gameCycle.StartGame);
        }

        public void Disable(IView view)
        {
            view.GetStartButton().onClick.RemoveListener(this.gameCycle.StartGame);
        }
    }
}