using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public class AtomicTimer : IAtomicTimer, IAtomicTickable
    {
        public event Action OnStarted;
        public event Action OnTimeChanged;
        public event Action OnStopped;
        public event Action OnFinished;
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
            get { return this.currentTime / this.duration; }
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
        public float CurrentTime
        {
            get { return this.currentTime; }
            set { this.currentTime = Mathf.Clamp(value, 0, this.duration); }
        }

        [Space]
        [SerializeField]
        private float duration;

        private float currentTime;

        public AtomicTimer()
        {
        }

        public AtomicTimer(float duration)
        {
            this.duration = duration;
            this.currentTime = 0;
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
        
        public void Reset()
        {
            this.currentTime = 0;
            this.OnReset?.Invoke();
        }

        private void SetProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);
            this.currentTime = this.duration * progress;
            this.OnTimeChanged?.Invoke();
        }

        public void Tick(float deltaTime)
        {
            if (this.currentTime < this.duration)
            {
                this.currentTime += deltaTime;
                this.OnTimeChanged?.Invoke();
            }
            else
            {
                this.IsPlaying = false;
                this.OnFinished?.Invoke();                
            }
        }
    }
}