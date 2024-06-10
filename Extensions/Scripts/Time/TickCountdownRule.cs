using Atomic.Elements;
using Atomic.Objects;

namespace Atomic.Extensions
{
    public sealed class TickCountdownRule : IEnable, IDisable, IUpdate, IFixedUpdate, ILateUpdate
    {
        private readonly AtomicCountdown countdown;
        private readonly TickMode tickMode;

        public TickCountdownRule(AtomicCountdown countdown, TickMode tickMode = TickMode.UPDATE)
        {
            this.countdown = countdown;
            this.tickMode = tickMode;
        }
        
        public void Enable()
        {
            this.countdown.Start();
        }

        public void Disable()
        {
            this.countdown.Stop();
        }

        public void OnUpdate(float deltaTime)
        {
            if (this.tickMode == TickMode.UPDATE)
            {
                this.countdown.Tick(deltaTime);
            }
        }
        
        public void OnFixedUpdate(float deltaTime)
        {
            if (this.tickMode == TickMode.FIXED_UPDATE)
            {
                this.countdown.Tick(deltaTime);
            }
        }

        public void OnLateUpdate(float deltaTime)
        {
            if (this.tickMode == TickMode.LATE_UPDATE)
            {
                this.countdown.Tick(deltaTime);
            }
        }
    }
}