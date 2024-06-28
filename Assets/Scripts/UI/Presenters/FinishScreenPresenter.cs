using System;
using Modules.Contexts;
using Modules.GameCycles;
using UnityEngine;

namespace SampleGame.UI
{
    [Serializable]
    public sealed class FinishScreenPresenter : IInitSystem, IDisposeSystem
    {
        [SerializeField]
        private FinishScreenView screenView;

        private GameCycle gameCycle;
        
        public void Init(IContext context)
        {
            this.screenView.Hide();

            this.gameCycle = context.GetGameCycle();
            this.gameCycle.OnGameFinished += this.OnGameFinished;
        }

        public void Dispose()
        {
            this.gameCycle.OnGameFinished -= this.OnGameFinished;
        }

        private void OnGameFinished()
        {
            this.screenView.Show();
        }
    }
}