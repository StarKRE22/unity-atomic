using System;
using Modules.Contexts;
using Modules.GameCycles;
using UnityEngine;

namespace SampleGame
{
    [Serializable]
    public sealed class GameCycleInputController : IInitSystem, IUpdateSystem
    {
        private GameCycle gameCycle;

        public void Init(IContext context)
        {
            this.gameCycle = context.GetGameCycle();
        }

        public void Update(IContext context, float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                this.gameCycle.StartGame();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                this.gameCycle.PauseGame();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                this.gameCycle.ResumeGame();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                this.gameCycle.FinishGame();
            }
        }
    }
}