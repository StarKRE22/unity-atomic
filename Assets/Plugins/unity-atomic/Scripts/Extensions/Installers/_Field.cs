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
        
        [HideLabel]
        [HorizontalGroup]
        [SerializeField]
        private T value;
        
        [GUIColor(0.5f, 0.5f, 0.5f)]
        [SerializeField]
        private bool optimized;

        public _Field()
        {
        }

        public _Field(T value)
        {
            this.value = value;
        }

        public T Value => this.value;

        public void Install(AtomicEntity entity)
        {
            if (this.optimized)
                entity.AddField(this.index, this.value);
            else
                entity.AddField(this.key, this.value);
        }
    }
}