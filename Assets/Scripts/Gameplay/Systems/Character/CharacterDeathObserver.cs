using System;
using Atomic.Contexts;
using Modules.GameCycles;

namespace SampleGame
{
    [Serializable]
    public sealed class CharacterDeathObserver : IInitSystem, IEnableSystem, IDisableSystem
    {
        private ICharacter character;
        private GameCycle gameCycle;

        public void Init(IContext context)
        {
            this.character = context.GetCharacter();
            this.gameCycle = context.GetGameCycle();
        }

        public void Enable(IContext context)
        {
            this.character.OnDeath += this.gameCycle.FinishGame;
        }

        public void Disable(IContext context)
        {
            this.character.OnDeath -= this.gameCycle.FinishGame;
        }
    }
}