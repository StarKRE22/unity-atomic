using System;
using Atomic.Objects;
using UnityEngine;

namespace Sample
{
    [Serializable]
    public sealed class DestroyableEffect : IEffect
    {
        [SerializeField]
        private GameObject gameObject;
        
        public void Apply(IAtomicObject obj)
        {
            //Nothing...
        }

        public void Discard(IAtomicObject obj)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}