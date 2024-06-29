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
        private int characterKey;

        [ContextKey]
        [SerializeField]
        private int moveInputKey;
        
        private ICharacter _character;
        private MoveInput _moveInput;

        public void Init(IContext context)
        {
            _character = context.GetValue<ICharacter>(this.characterKey);
            _moveInput = context.GetValue<MoveInput>(this.moveInputKey);
            _moveInput.OnMove += _character.Move;
        }

        public void Dispose(IContext context)
        {
            _moveInput.OnMove -= _character.Move;
        }
    }
}