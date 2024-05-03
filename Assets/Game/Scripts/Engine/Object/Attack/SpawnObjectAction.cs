using System;
using Atomic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Content
{
    [Serializable]
    public sealed class SpawnObjectAction : IAtomicAction
    {
        private Transform firePosition;
        private GameObject prefab;

        public void Compose(Transform firePosition, GameObject prefab)
        {
            this.firePosition = firePosition;
            this.prefab = prefab;
        }
        
        [Button]
        public void Invoke()
        {
            Vector3 position = this.firePosition.position;
            Quaternion rotation = this.firePosition.rotation;
            GameObject.Instantiate(this.prefab, position, rotation, null);
        }
    }
}