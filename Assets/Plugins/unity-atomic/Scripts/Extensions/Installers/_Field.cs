using System;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_CONTRACTS
using Contracts;
#endif

namespace Atomic.Installers
{
    [Serializable]
    public class _Field<T> : AtomicEntity.IInstaller
    {
#if UNITY_CONTRACTS
            [ContractValue]
#endif
        [HorizontalGroup]
        [SerializeField]
        private int index = -1;
        
        [HideLabel]
        [HorizontalGroup]
        [SerializeField]
        public T value;

        public _Field()
        {
        }

        public _Field(T value)
        {
            this.value = value;
        }

        public void Install(AtomicEntity entity)
        {
            entity.Put(this.index, this.value);
        }
    }
}