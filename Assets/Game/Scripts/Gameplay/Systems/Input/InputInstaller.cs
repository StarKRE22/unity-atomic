using System;
using Atomic.Contexts;
using SampleGame;
using UnityEngine;

namespace Modules.Gameplay
{
    [Serializable]
    public sealed class InputInstaller : IContextInstaller
    {
        [SerializeField]
        private MoveInputConfig inputConfig;
        
        public void Install(IContext context)
        {
            context.AddMoveInput(new MoveInput(this.inputConfig));
            context.AddSystem<MoveInputUpdater>();
        }
    }
}