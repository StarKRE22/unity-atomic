using System;
using Atomic.Contexts;
using Modules.Gameplay;

namespace SampleGame
{
    [Serializable]
    public sealed class MoveInputUpdater : IInitSystem, IDisposeSystem, IGameTickable
    {
        private MoveInput moveInput;
        private GameCycle gameCycle;

        public void Init(IContext context)
        {
            this.moveInput = context.GetMoveInput();

            this.gameCycle = context.GetGameCycle();
            this.gameCycle.AddListener(this);
        }

        public void Dispose(IContext context)
        {
            this.gameCycle.DelListener(this);
        }
        
        public void Tick(float deltaTime)
        {
            this.moveInput.Update();
        }
    }
}