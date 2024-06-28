using System;
using Modules.Contexts;
using SampleGame;
using UnityEngine;

namespace Modules.Inputs
{
    [Serializable]
    public sealed class InputInstaller : IContextInstaller
    {
        [SerializeField]
        private MoveInputConfig inputConfig;
        
        public void Install(IContext context)
        {
            context.AddMoveInput(new MoveInput(this.inputConfig));
            context.AddSystem<InputUpdater>();
        }
    }
}