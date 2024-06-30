using Atomic.Contexts;
using Modules.Gameplay;

namespace SampleGame
{
    public sealed class GameCycleUpdater : 
        IUpdateSystem,
        IFixedUpdateSystem,
        ILateUpdateSystem
    {
        private GameCycle gameCycle;

        [Construct]
        public void Construct([Inject(GameplayAPI.GameCycle)] GameCycle gameCycle)
        {
            this.gameCycle = gameCycle;
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