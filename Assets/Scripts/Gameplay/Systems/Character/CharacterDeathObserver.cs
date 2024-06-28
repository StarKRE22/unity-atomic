using System;
using Atomic.Contexts;
using Modules.GameCycles;

namespace SampleGame
{
    [Serializable]
    public sealed class CharacterDeathObserver : IEnableSystem, IDisableSystem
    {
        private ICharacter character;
        private GameCycle gameCycle;

        [Construct]
        public void Construct(
            [Inject(GameContextAPI.Character)]
            ICharacter character,
            [Inject(GameContextAPI.GameCycle)]
            GameCycle gameCycle
        )
        {
            this.character = character;
            this.gameCycle = gameCycle;
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