using System;
using Atomic.Contexts;
using Modules.Gameplay;
using UnityEngine;

namespace SampleGame
{
    [Serializable]
    public sealed class CharacterMoveController : IInitSystem, IDisposeSystem
    {
        [ContextKey]
        [SerializeField]
        private int characterKey = GameContextAPI.Character;
        
        private ICharacter character;
        private MoveInput moveInput;

        public void Init(IContext context)
        {
            this.character = context.GetValue<ICharacter>(this.characterKey);
            this.moveInput = context.GetMoveInput();
            this.moveInput.OnMove += this.character.Move;
        }

        public void Dispose(IContext context)
        {
            this.moveInput.OnMove -= this.character.Move;
        }
    }
}