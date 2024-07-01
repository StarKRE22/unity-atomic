using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.AI
{
    [Serializable, InlineProperty]
    public abstract class ValueInstaller<T> : IBlackboardInstaller
    {
        [SerializeField, BlackboardKey]
        protected int key;

        [SerializeField]
        protected T value;

        public abstract void Install(IBlackboard blackboard);
    }
}