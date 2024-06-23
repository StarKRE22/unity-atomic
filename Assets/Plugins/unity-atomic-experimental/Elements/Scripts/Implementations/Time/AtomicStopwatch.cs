using System;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public class AtomicStopwatch : IAtomicPlayable, IAtomicTime, IAtomicResetable, IAtomicTickable
    {
        public event Action OnStarted;
        public event Action OnStopped;
        public event Action<float> OnTimeChanged;
        public event Action OnReset;

        private float currentTime;
        private bool isPlaying;

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
            float newTime = Mathf.Max(time, 0);

            if (Math.Abs(currentTime - newTime) > float.Epsilon)
            {
                this.currentTime = newTime;
                this.OnTimeChanged?.Invoke(time);
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
                Debug.LogWarning("Stopwatch is not playing!");
                return;
            }

            this.currentTime += deltaTime;
            this.OnTimeChanged?.Invoke(this.currentTime);
        }

        public void Reset()
        {
            this.currentTime = 0;
            this.OnReset?.Invoke();
        }
    }
}