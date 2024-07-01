using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.AI
{
    [Serializable, InlineProperty]
    public abstract class BlackboardInstaller<T> : IBlackboardInstaller
    {
        [HorizontalGroup]
        [SerializeField, BlackboardKey]
        protected int key;

        [HorizontalGroup]
        [SerializeField]
        protected T value;

        public abstract void Install(IBlackboard blackboard);
    }
}