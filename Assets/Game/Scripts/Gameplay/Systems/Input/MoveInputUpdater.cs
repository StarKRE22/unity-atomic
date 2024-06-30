using System;
using Atomic.Contexts;
using Modules.Gameplay;

namespace SampleGame
{
    [Serializable]
    public sealed class MoveInputUpdater : IInitSystem, IDisposeSystem, IGameTickable
    {
        [Inject(GameplayAPI.MoveInput)]
        private MoveInput moveInput;
        
        [Inject(GameplayAPI.GameCycle)]
        private GameCycle gameCycle;

        public void Init(IContext context)
        {
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