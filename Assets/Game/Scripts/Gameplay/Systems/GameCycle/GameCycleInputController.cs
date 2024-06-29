using System;
using Atomic.Contexts;
using Modules.Gameplay;
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
                this.gameCycle.Start();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                this.gameCycle.Pause();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                this.gameCycle.Resume();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                this.gameCycle.Finish();
            }
        }
    }
}