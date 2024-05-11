using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public class AtomicPeriod : IAtomicPeriod, IAtomicTickable
    {
        public event Action OnStarted;
        public event Action OnPeriod;
        public event Action OnStopped;

        [PropertyOrder(-10)]
        [PropertySpace]
        [ReadOnly]
        [ShowInInspector]
        public bool IsPlaying
        {
            get { return this.isPlaying; }
        }

        public float Duration
        {
            get { return this.period; }
            set { this.period = value; }
        }

        [SerializeField]
        private float period;
        private float acc;

        private bool isPlaying;

        public AtomicPeriod()
        {
        }

        public AtomicPeriod(float period)
        {
            this.period = period;
        }

        public void Start()
        {
            if (!this.isPlaying)
            {
                this.isPlaying = true;
                this.OnStarted?.Invoke();
            }
        }

        public void Stop()
        {
            if (this.isPlaying)
            {
                this.isPlaying = false;
                this.OnStopped?.Invoke();
            }
        }

        public void Tick(float deltaTime)
        {
            if (!this.isPlaying)
            {
                return;
            }
            
            this.acc += deltaTime;
            
            if (this.acc >= this.period)
            {
                this.OnPeriod?.Invoke();
                this.acc -= this.period;
            }
        }
    }
}