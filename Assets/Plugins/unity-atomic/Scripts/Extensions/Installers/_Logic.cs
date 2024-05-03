using System;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_CONTRACTS
using Contracts;
#endif

namespace Atomic.Installers
{
    [Serializable]
    public class _Logic<T> : AtomicObject.IInstaller where T : IAtomicLogic
    {
        public enum KeyMode
        {
            NONE = 0,
            KEY = 1,
            INDEX = 2
        }

#if UNITY_CONTRACTS
            [ContractValue]
#endif
        [ShowIf("keyMode", "KeyMode.KEY")]
        [HorizontalGroup]
        [SerializeField]
        private string key = "Enter Key";

#if UNITY_CONTRACTS
            [ContractValue]
#endif
        [HorizontalGroup]
        [ShowIf("keyMode", "KeyMode.INDEX")]
        [SerializeField]
        private int index = -1;

        [HorizontalGroup]
        [HideLabel]
        [SerializeField]
        private T value;
        
        [GUIColor(0.5f, 0.5f, 0.5f)]
        [SerializeField]
        private KeyMode keyMode;
        
        public void Install(AtomicObject obj)
        {
            obj.AddLogic(this.value);

            if (this.keyMode == KeyMode.NONE)
                return;

            if (this.keyMode == KeyMode.KEY)
                obj.AddField(this.key, this.value);
            else
                obj.AddField(this.index, this.value);
        }
    }
}