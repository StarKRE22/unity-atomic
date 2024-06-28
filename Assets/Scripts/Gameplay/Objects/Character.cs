using System;
using Unity.VisualScripting;
using UnityEngine;

namespace SampleGame
{
    public sealed class Character : MonoBehaviour, ICharacter
    {
        public event Action OnDeath;
        
        [SerializeField]
        private float speed = 2.5f;

        public void Move(Vector3 direction, float deltaTime)
        {
            this.transform.position += direction * (deltaTime * this.speed);
        }

        public Vector3 GetPosition()
        {
            return this.transform.position;
        }

        [ContextMenu("Death")]
        public void Death()
        {
            this.gameObject.SetActive(false);
            this.OnDeath?.Invoke();
        }
    }
}