using System;
using Contracts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Installers
{
    [Serializable]
    public class _Logic_Id<T>  where T : IAtomicLogic 
    {
#if UNITY_CONTRACTS
        [ContractValue]
#endif
        [HideIf(nameof(optimized))]
        [HorizontalGroup]
        [SerializeField]
        private string key = "Enter Key";

#if UNITY_CONTRACTS
        [ContractValue]
#endif
        [HorizontalGroup]
        [ShowIf(nameof(optimized))]
        [SerializeField]
        private int index = -1;

        [GUIColor(0.5f, 0.5f, 0.5f)]
        [SerializeField]
        private bool optimized;
        
        public void Install(AtomicObject obj, T value)
        {
            obj.AddLogic(value);

            if (this.optimized)
                obj.AddField(this.index, value);
            else
                obj.AddField(this.key, value);
        }
    }
}