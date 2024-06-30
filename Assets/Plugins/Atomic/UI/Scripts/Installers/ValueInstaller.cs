using System;
using UnityEngine;

namespace Atomic.UI
{
    [Serializable]
    public class ValueInstaller<T> : IViewInstaller 
    {
        [ViewKey, SerializeField]
        public int key;

        [SerializeField]
        private T value;
        
        public void Install(IView view)
        {
            view.AddValue(this.key, this.value);
        }
    }
}