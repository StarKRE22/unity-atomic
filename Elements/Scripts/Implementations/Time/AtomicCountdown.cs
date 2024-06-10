using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public class AtomicCountdown : IAtomicPlayable, IAtomicProgress, IAtomicResetable, IAtomicEnd, IAtomicTickable
    {
        public event Action OnStarted;
        public event Action OnStopped;

        public event Action<float> OnTimeChanged;
        public event Action<float> OnDurationChanged;
        public event Action<float> OnProgressChanged;

        public event Action OnEnded;
        public event Action OnReset;
        
        [SerializeField]
        private float duration;

        [SerializeField]
        private float remainingTime;
        
        [ShowInInspector, ReadOnly]
        private bool isPlaying;

        public AtomicCountdown()
        {
        }

        public AtomicCountdown(float duration)
        {
            this.duration = duration;
            this.remainingTime = duration;
        }
        
        public bool IsPlaying()
        {
            return this.isPlaying;
        }

        public bool IsEnded()
        {
            return this.remainingTime <= 0;
        }

        public float GetProgress()
        {
            return 1 - this.remainingTime / this.duration;
        }
        
        public void SetProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);
            float newTime = this.duration * (1 - progress);
            this.remainingTime = newTime;
            this.OnTimeChanged?.Invoke(newTime);
            this.OnProgressChanged?.Invoke(progress);
        }
        
        public float GetDuration()
        {
            return this.duration;
        }
        
        public void SetDuration(float duration)
        {
            if (duration < 0)
            {
                return;
            }
            
            if (Math.Abs(this.duration - duration) > float.Epsilon)
            {
                this.duration = duration;
                this.OnDurationChanged?.Invoke(duration);
            }
        }

        public float GetTime()
        {
            return this.remainingTime;
        }

        public void SetTime(float time)
        {
            if (time < 0)
            {
                return;
            }
            
            float newTime = Mathf.Clamp(time, 0, this.duration);
            if (Math.Abs(newTime - this.remainingTime) > float.Epsilon)
            {
                this.remainingTime = newTime;
                this.OnTimeChanged?.Invoke(newTime);
                this.OnProgressChanged?.Invoke(this.GetProgress());
            }
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
                Debug.LogWarning("Countdown is not playing!");
                return;
            }
            
            if (this.remainingTime > 0)
            {
                this.remainingTime -= deltaTime;
                this.OnTimeChanged?.Invoke(this.remainingTime);
                this.OnProgressChanged?.Invoke(this.GetProgress());
            }
            else
            {
                this.OnEnded?.Invoke();                
            }
        }

        public void Reset()
        {
            this.remainingTime = this.duration;
            this.OnReset?.Invoke();
        }
    }
}