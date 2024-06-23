using Atomic.Elements;
using Atomic.Objects;

namespace Atomic.Extensions
{
    public sealed class TickTimerRule : IEnable, IDisable, IUpdate, IFixedUpdate, ILateUpdate
    {
        private readonly AtomicTimer timer;
        private readonly TickMode tickMode;

        public TickTimerRule(AtomicTimer timer, TickMode tickMode = TickMode.UPDATE)
        {
            this.timer = timer;
            this.tickMode = tickMode;
        }
        
        public void Enable()
        {
            this.timer.Start();
        }

        public void Disable()
        {
            this.timer.Stop();
        }

        public void OnUpdate(float deltaTime)
        {
            if (this.tickMode == TickMode.UPDATE)
            {
                this.timer.Tick(deltaTime);
            }
        }
        
        public void OnFixedUpdate(float deltaTime)
        {
            if (this.tickMode == TickMode.FIXED_UPDATE)
            {
                this.timer.Tick(deltaTime);
            }
        }

        public void OnLateUpdate(float deltaTime)
        {
            if (this.tickMode == TickMode.LATE_UPDATE)
            {
                this.timer.Tick(deltaTime);
            }
        }
    }
}