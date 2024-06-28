using System;
using UnityEngine;

namespace Atomic.UI
{
    [Serializable]
    public class DataInstaller<T> : IViewInstaller 
    {
        [ViewKey, SerializeField]
        public int key;

        [SerializeField]
        private T value;
        
        public void Install(IView view)
        {
            view.AddData(this.key, this.value);
        }
    }
}