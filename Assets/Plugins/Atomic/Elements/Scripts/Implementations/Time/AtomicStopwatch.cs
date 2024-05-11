using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public class AtomicStopwatch : IAtomicStopwatch, IAtomicTickable
    {
        public event Action OnStarted;
        public event Action OnTimeChanged;
        public event Action OnStopped;
        public event Action OnReset;

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-10)]
        [PropertySpace(8)]
        public bool IsPlaying { get; private set; }

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-8)]
        public float CurrentTime
        {
            get { return this.currentTime; }
            set { this.currentTime = Mathf.Max(value, 0); }
        }

        private float currentTime;

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
            if (this.IsPlaying)
            {
                this.currentTime += deltaTime;
                this.OnTimeChanged?.Invoke();
            }
        }

        public void Reset()
        {
            this.currentTime = 0;
            this.OnReset?.Invoke();
        }
    }
}