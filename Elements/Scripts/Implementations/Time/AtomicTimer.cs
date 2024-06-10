using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public class AtomicTimer : IAtomicPlayable, IAtomicProgress, IAtomicResetable, IAtomicEnd, IAtomicTickable
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
        private float currentTime;
        
        [ShowInInspector, ReadOnly]
        private bool isPlaying;

        public AtomicTimer()
        {
        }

        public AtomicTimer(float duration)
        {
            this.duration = duration;
            this.currentTime = 0;
        }

        public bool IsPlaying()
        {
            return this.isPlaying;
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
            
            float newTime = Mathf.Clamp(time, 0, this.duration);
            if (Math.Abs(newTime - this.currentTime) > float.Epsilon)
            {
                this.currentTime = newTime;
                this.OnTimeChanged?.Invoke(newTime);
                this.OnProgressChanged?.Invoke(this.GetProgress());
            }
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

        public bool IsEnded()
        {
            return this.currentTime >= this.duration;
        }
        
        public float GetProgress()
        {
            return this.currentTime / this.duration;
        }

        public void SetProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);
            this.currentTime = this.duration * progress;
            this.OnTimeChanged?.Invoke(this.currentTime);
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
        
        public void Reset()
        {
            this.currentTime = 0;
            this.OnReset?.Invoke();
        }
        
        public void Tick(float deltaTime)
        {
            if (!this.isPlaying)
            {
                Debug.LogWarning("Timer is not playing!");
                return;
            }
            
            if (this.currentTime < this.duration)
            {
                this.currentTime += deltaTime;
                this.OnTimeChanged?.Invoke(this.currentTime);
                this.OnProgressChanged?.Invoke(this.GetProgress());
            }
            else
            {
                this.OnEnded?.Invoke();                
            }
        }
    }
}