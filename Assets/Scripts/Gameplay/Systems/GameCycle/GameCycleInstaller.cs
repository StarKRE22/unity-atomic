using System;
using Atomic.Contexts;
using Modules.GameCycles;

namespace SampleGame
{
    [Serializable]
    public sealed class GameCycleInstaller : IContextInstaller
    {
        public void Install(IContext context)
        {
            context.AddGameCycle(new GameCycle());
            context.AddSystem<GameCycleInputController>();
            context.AddSystem<GameTimeDebug>();
        }
    }
}