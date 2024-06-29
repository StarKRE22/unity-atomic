using System;
using Atomic.Contexts;
using Modules.Gameplay;

namespace SampleGame
{
    [Serializable]
    public sealed class CharacterDeathObserver : IInitSystem, IDisposeSystem
    {
        private ICharacter character;
        private GameCycle gameCycle;

        public void Init(IContext context)
        {
            this.gameCycle = context.ResolveGameCycle();
            this.character = context.GetCharacter();
            this.character.OnDeath += this.gameCycle.Finish;
        }

        public void Dispose(IContext context)
        {
            this.character.OnDeath -= this.gameCycle.Finish;
        }
    }
}