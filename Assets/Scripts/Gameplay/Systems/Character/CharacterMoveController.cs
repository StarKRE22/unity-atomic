using System;
using Modules.Contexts;
using Modules.Inputs;

namespace SampleGame
{
    [Serializable]
    public sealed class CharacterMoveController : IInitSystem, IEnableSystem, IDisableSystem
    {
        private ICharacter character;
        private MoveInput moveInput;

        public void Init(IContext context)
        {
            this.character = context.GetCharacter();
            this.moveInput = context.GetMoveInput();
        }
        
        public void Enable(IContext context)
        {
            this.moveInput.OnMove += this.character.Move;
        }

        public void Disable(IContext context)
        {
            this.moveInput.OnMove -= this.character.Move;
        }
    }
}