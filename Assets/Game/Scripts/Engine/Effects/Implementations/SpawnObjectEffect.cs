using System;
using Atomic;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class SpawnObjectEffect : IEffect
    {
        [SerializeField]
        private GameObject prefab;

        private GameObject _gameObject;
        
        public void Apply(IAtomicObject obj)
        {
            if (obj is Component component)
            {
                Transform transform = component.transform;
                _gameObject = GameObject.Instantiate(this.prefab, transform.position, transform.rotation, transform);
            }
        }

        public void Discard(IAtomicObject obj)
        {
            GameObject.Destroy(_gameObject);
        }
    }
}