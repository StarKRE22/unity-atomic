using Atomic.Contexts;
using Modules.GameCycles;

namespace SampleGame
{
    public sealed class GameCycleUpdater : IInitSystem,
        IUpdateSystem,
        IFixedUpdateSystem,
        ILateUpdateSystem
    {
        private GameCycle gameCycle;

        public void Init(IContext context)
        {
            this.gameCycle = context.GetGameCycle();
        }

        public void Update(IContext context, float deltaTime)
        {
            if (this.gameCycle.IsPlaying())
            {
                this.gameCycle.Tick(deltaTime);
            }
        }

        public void FixedUpdate(IContext context, float deltaTime)
        {
            if (this.gameCycle.IsPlaying())
            {
                this.gameCycle.FixedTick(deltaTime);
            }
        }

        public void LateUpdate(IContext context, float deltaTime)
        {
            if (this.gameCycle.IsPlaying())
            {
                this.gameCycle.LateTick(deltaTime);
            }
        }
    }
}