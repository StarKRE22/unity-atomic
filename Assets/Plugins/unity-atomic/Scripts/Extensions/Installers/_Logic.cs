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
#if UNITY_CONTRACTS
            [ContractValue]
#endif
        [HorizontalGroup]
        [ShowIf(nameof(hasId))]
        [SerializeField]
        private int index = -1;

        [HorizontalGroup]
        [HideLabel]
        [SerializeField]
        private T value;

        [GUIColor(0.5f, 0.5f, 0.5f)]
        [SerializeField]
        private bool hasId;

        public void Install(AtomicObject obj)
        {
            if (this.hasId) obj.Put(this.index, this.value);
            obj.AddLogic(this.value);
        }
    }
}