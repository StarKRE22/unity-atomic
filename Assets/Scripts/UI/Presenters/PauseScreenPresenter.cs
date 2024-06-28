using System;
using Modules.Contexts;
using Modules.GameCycles;
using SampleGame.App;
using UnityEngine;

namespace SampleGame.UI
{
    [Serializable]
    public sealed class PauseScreenPresenter : IInitSystem, IDisposeSystem
    {
        [SerializeField]
        private PauseScreenView screenView;
        
        private GameCycle gameCycle;
        
        public void Init(IContext context)
        {
            this.gameCycle = context.GetGameCycle();
            this.gameCycle.OnGamePaused += this.screenView.Show;
            this.gameCycle.OnGameResumed += this.screenView.Hide;
            
            this.screenView.resumeGameButton.onClick.AddListener(this.gameCycle.ResumeGame);
            this.screenView.finishGameButton.onClick.AddListener(this.gameCycle.FinishGame);
            this.screenView.exitAppButton.onClick.AddListener(ApplicationCase.Exit);
            
            this.screenView.Hide();
        }

        public void Dispose()
        {
            this.gameCycle.OnGamePaused -= this.screenView.Show;
            this.gameCycle.OnGameResumed -= this.screenView.Hide;
            
            this.screenView.resumeGameButton.onClick.RemoveListener(this.gameCycle.ResumeGame);
            this.screenView.finishGameButton.onClick.RemoveListener(this.gameCycle.FinishGame);
            this.screenView.exitAppButton.onClick.RemoveListener(ApplicationCase.Exit);
        }
    }
}