using System;
using UnityEngine;

namespace Modules.AI
{
    [Serializable]
    public sealed class TagInstaller : IBlackboardInstaller
    {
        [BlackboardKey]
        [SerializeField]
        private int tag;
        
        public void Install(IBlackboard blackboard)
        {
            blackboard.SetTag(this.tag);
        }
    }
}