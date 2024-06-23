using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public class AtomicPeriod : IAtomicPlayable, IAtomicProgress, IAtomicTickable
    {
        public event Action OnStarted;
        public event Action OnStopped;
        public event Action OnPeriod;

        public event Action<float> OnTimeChanged;
        public event Action<float> OnProgressChanged;
        public event Action<float> OnDurationChanged;

        [SerializeField]
        private float period;

        [ShowInInspector, ReadOnly]
        private float currentTime;

        [ShowInInspector, ReadOnly]
        private bool isPlaying;

        public AtomicPeriod()
        {
        }

        public AtomicPeriod(float period)
        {
            this.period = period;
        }

        public bool IsPlaying()
        {
            return this.isPlaying;
        }

        public float GetDuration()
        {
            return this.period;
        }

        public void SetDuration(float duration)
        {
            if (duration < 0)
            {
                return;
            }
            
            if (Math.Abs(this.period - duration) > float.Epsilon)
            {
                this.period = duration;
                this.OnDurationChanged?.Invoke(duration);
            }
        }
        
        public float GetTime()
        {
            return this.currentTime;
        }

        public void SetTime(float time)
        {
            if (time < 0)
            {
                return;
            }
            
            float newTime = Mathf.Clamp(time, 0, this.period);
            if (Math.Abs(newTime - this.period) > float.Epsilon)
            {
                this.currentTime = newTime;
                this.OnTimeChanged?.Invoke(newTime);
            }
        }

        public float GetProgress()
        {
            return this.currentTime / this.period;
        }

        public void SetProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);
            float newTime = this.period * progress;
            this.currentTime = newTime;
            this.OnTimeChanged?.Invoke(newTime);
            this.OnProgressChanged?.Invoke(progress);
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
                Debug.LogWarning("Period is not playing!");
                return;
            }

            this.currentTime += deltaTime;

            if (this.currentTime >= this.period)
            {
                this.OnPeriod?.Invoke();
                this.currentTime -= this.period;
            }
        }
    }
}