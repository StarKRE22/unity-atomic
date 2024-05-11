using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public class AtomicCountdown : IAtomicCountdown, IAtomicTickable
    {
        public event Action OnStarted;
        public event Action OnStopped;
        public event Action OnTimeChanged;
        public event Action OnEnded;
        public event Action OnReset;

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-10)]
        [PropertySpace(8)]
        public bool IsPlaying { get; private set; }

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-9)]
        [ProgressBar(0, 1)]
        public float Progress
        {
            get { return 1 - this.remainingTime / this.duration; }
            set { this.SetProgress(value); }
        }

        public float Duration
        {
            get { return this.duration; }
            set { this.duration = value; }
        }

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-8)]
        public float RemainingTime
        {
            get { return this.remainingTime; }
            set { this.remainingTime = Mathf.Clamp(value, 0, this.duration); }
        }

        [Space]
        [SerializeField]
        private float duration;

        private float remainingTime;

        public AtomicCountdown()
        {
        }

        public AtomicCountdown(float duration)
        {
            this.duration = duration;
            this.remainingTime = duration;
        }

        public void Start()
        {
            if (!this.IsPlaying)
            {
                this.IsPlaying = true;
                this.OnStarted?.Invoke();
            }
        }
        
        public void Stop()
        {
            if (this.IsPlaying)
            {
                this.IsPlaying = false;
                this.OnStopped?.Invoke();
            }
        }
        
        public void Tick(float deltaTime)
        {
            if (!this.IsPlaying)
            {
                return;
            }
            
            if (this.remainingTime > 0)
            {
                this.remainingTime -= deltaTime;
                this.OnTimeChanged?.Invoke();
            }
            else
            {
                this.IsPlaying = false;
                this.OnEnded?.Invoke();                
            }
        }

        public void Reset()
        {
            this.remainingTime = this.duration;
            this.OnReset?.Invoke();
        }

        private void SetProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);
            this.remainingTime = this.duration * (1 - progress);
            this.OnTimeChanged?.Invoke();
        }
    }
}