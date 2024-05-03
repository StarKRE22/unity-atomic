using System;
using Atomic;
using UnityEngine;

namespace Game.Engine
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