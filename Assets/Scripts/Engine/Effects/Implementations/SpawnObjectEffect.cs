using System;
using Atomic.Objects;
using GameEngine;
using UnityEngine;
using GameObject = UnityEngine.GameObject;

namespace Sample
{
    [Serializable]
    public sealed class SpawnObjectEffect : IEffect
    {
        [SerializeField]
        private GameObject prefab;

        private GameObject _gameObject;
        
        public void Apply(IObject obj)
        {
            Transform transform = obj.GetTransform();
            if (transform != null)
            {
                _gameObject = GameObject.Instantiate(this.prefab, transform.position, transform.rotation, transform);
            }
        }

        public void Discard(IObject obj)
        {
            if (_gameObject != null)
            {
                GameObject.Destroy(_gameObject);
                _gameObject = null;
            }
        }
    }
}