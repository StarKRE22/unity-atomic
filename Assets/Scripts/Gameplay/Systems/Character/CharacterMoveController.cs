using System;
using Atomic.Contexts;
using Modules.Inputs;

namespace SampleGame
{
    [Serializable]
    public sealed class CharacterMoveController : IInitSystem, IDisposeSystem
    {
        private ICharacter character;
        private MoveInput moveInput;

        public void Init(IContext context)
        {
            this.character = context.GetCharacter();
            this.moveInput = context.GetMoveInput();
            this.moveInput.OnMove += this.character.Move;
        }

        public void Dispose(IContext context)
        {
            this.moveInput.OnMove -= this.character.Move;
        }
    }
}