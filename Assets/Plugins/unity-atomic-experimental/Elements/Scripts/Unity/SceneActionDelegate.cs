using System;
using Atomic.Elements;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public sealed class SceneActionDelegate : IAtomicAction
    {
        [SerializeField]
        private SceneAction action;
        
        public void Invoke()
        {
            this.action.Invoke();
        }
    }
}