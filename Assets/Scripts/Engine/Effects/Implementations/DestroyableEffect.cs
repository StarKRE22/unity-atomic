using System;
using Atomic.Objects;
using UnityEngine;
using GameObject = UnityEngine.GameObject;

namespace Sample
{
    [Serializable]
    public sealed class DestroyableEffect : IEffect
    {
        [SerializeField]
        private GameObject gameObject;
        
        public void Apply(IObject obj)
        {
            //Nothing...
        }

        public void Discard(IObject obj)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}