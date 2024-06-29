using System;
using Atomic.Contexts;
using UnityEngine;

namespace SampleGame
{
    [Serializable]
    public sealed class CharacterInstaller : IContextInstaller
    {
        [SerializeField]
        private Character character;
        
        public void Install(IContext context)
        {
            context.AddCharacter(this.character);
            context.AddSystem<CharacterDeathObserver>();
            // context.AddSystem<CharacterMoveController>();
        }
    }
}