using System;
using Contracts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Installers
{
    [Serializable]
    public sealed class _Field_Id<T>
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

        public void Install(AtomicEntity entity, T value)
        {
            if (this.optimized)
                entity.AddField(this.index, value);
            else
                entity.AddField(this.key, value);
        }
    }
}