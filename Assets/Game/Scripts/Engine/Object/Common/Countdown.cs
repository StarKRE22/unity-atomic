using System;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public class Countdown
    {
        public float duration;
        public float time;

        public Countdown(float duration)
        {
            this.duration = duration;
            this.time = duration;
        }

        public void Tick(float deltaTime)
        {
            this.time = Mathf.Max(0, this.time - deltaTime);
        }

        public void Reset()
        {
            this.time = this.duration;
        }

        public bool IsEnded()
        {
            return this.time <= 0;
        }
    }
}