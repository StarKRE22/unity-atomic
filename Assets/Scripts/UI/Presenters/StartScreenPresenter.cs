using System;
using Modules.Contexts;
using Modules.GameCycles;
using UnityEngine;

namespace SampleGame.UI
{
    [Serializable]
    public sealed class StartScreenPresenter : IInitSystem, IDisposeSystem
    {
        [SerializeField]
        private StartScreenView startScreen;
        
        private GameCycle gameCycle;

        public void Init(IContext context)
        {
            this.gameCycle = context.GetGameCycle();
            this.gameCycle.OnGameStarted += this.OnGameStarted;
            
            this.startScreen.startButton.onClick.AddListener(this.gameCycle.StartGame);

            this.startScreen.Show();
        }

        public void Dispose()
        {
            this.gameCycle.OnGameStarted -= this.OnGameStarted;
            this.startScreen.startButton.onClick.RemoveListener(this.gameCycle.StartGame);
        }

        private void OnGameStarted()
        {
            this.startScreen.Hide();
        }
    }
}