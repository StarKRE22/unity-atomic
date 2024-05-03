using System;
using AIModule;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Engine
{
    [Serializable]
    public sealed class BTNode_WaitForSeconds : BTNode
    {
        public override string Name => "Wait For Seconds";

        [SerializeField]
        private float duration = 1;
        
        [SerializeField, HideInInspector]
        private float startTime;

        protected override void OnEnable(IBlackboard blackboard)
        {
            base.OnEnable(blackboard);
            this.startTime = Time.time;
        }

        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            return Time.time - this.startTime >= this.duration
                ? BTState.SUCCESS
                : BTState.RUNNING;
        }
    }
}