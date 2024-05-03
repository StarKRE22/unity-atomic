using System;
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
        [SerializeField]
        private int index = -1;

        public void Install(AtomicEntity bundle)
        {
            bundle.Mark(this.index);
        }
    }
}