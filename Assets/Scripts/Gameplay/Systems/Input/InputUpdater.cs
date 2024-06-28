using System;
using Atomic.Contexts;
using Modules.GameCycles;
using Modules.Inputs;

namespace SampleGame
{
    [Serializable]
    public sealed class InputUpdater : IInitSystem, IUpdateSystem
    {
        private MoveInput moveInput;
        private GameCycle gameCycle;
        
        public void Init(IContext context)
        {
            this.moveInput = context.GetMoveInput();
            this.gameCycle = context.GetGameCycle();
        }

        public void Update(IContext context, float deltaTime)
        {
            if (this.gameCycle.State == GameState.PLAY)
            {
                this.moveInput.Update(deltaTime);
            }
        }
    }
}