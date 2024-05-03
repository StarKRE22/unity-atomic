using System;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_CONTRACTS
using Contracts;
#endif

namespace Atomic.Installers
{
    [Serializable]
    public sealed class _Type : AtomicEntity.IInstaller
    {
#if UNITY_CONTRACTS
        [ContractType]
#endif
        [HideIf(nameof(optimized))]
        [SerializeField]
        private string key = "Enter Key";

#if UNITY_CONTRACTS
        [ContractType]
#endif
        [ShowIf(nameof(optimized))]
        [SerializeField]
        private int index = -1;
        
        [GUIColor(0.5f, 0.5f, 0.5f)]
        [SerializeField]
        private bool optimized;

        public void Install(AtomicEntity bundle)
        {
            if (this.optimized)
                bundle.AddType(this.index);
            else
                bundle.AddType(this.key);
        }
    }
}