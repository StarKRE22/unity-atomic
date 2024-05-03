using System;
using Atomic;
using UnityEngine;

namespace Sample
{
    public sealed class Character : AtomicEntity
    {
        [Get(ObjectAPI.GameObject)]
        public GameObject go;

        [Get(ObjectAPI.Transform)]
        public Transform tran;

        private void Start()
        {
            GameObject go = this.Get<GameObject>(ObjectAPI.GameObject);
            Debug.Log($"{go.name}");
        }
    }
}