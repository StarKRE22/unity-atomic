using System;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public class HitPoints
    {
        public event Action<int> OnChanged;

        [SerializeField]
        private int maxHitPoints;

        [SerializeField]
        private int hitPoints;

        public int Current
        {
            get { return hitPoints; }
            set
            {
                value = Mathf.Clamp(value, 0, maxHitPoints);
                hitPoints = value;
                OnChanged?.Invoke(value);
            }
        }

        public int Max
        {
            get { return maxHitPoints; }
            set
            {
                value = Math.Max(1, value);
                if (hitPoints > value)
                    hitPoints = value;

                maxHitPoints = value;
            }
        }

        public float Percent => (float) this.hitPoints / this.maxHitPoints;

        public HitPoints()
        {
        }

        public HitPoints(int current, int max)
        {
            hitPoints = current;
            maxHitPoints = max;
        }


        public void Restore()
        {
            hitPoints = maxHitPoints;
        }
    }
}