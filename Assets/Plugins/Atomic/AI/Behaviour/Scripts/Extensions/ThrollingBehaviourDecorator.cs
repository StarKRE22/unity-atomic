using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Modules.AI
{
    [Serializable]
    public sealed class ThrollingBehaviourDecorator : IStartBehaviour, IStopBehaviour, IUpdateBehaviour
    {
        [SerializeField]
        private float minScanPeriod = 0.2f;

        [SerializeField]
        private float maxScanPeriod = 0.3f;

        private float currentTime;

        [SerializeReference]
        private IUpdateBehaviour behaviour = default;

        public void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (this.currentTime > 0)
            {
                this.currentTime -= deltaTime;
            }
            else
            {
                this.behaviour?.OnUpdate(blackboard, deltaTime);
                this.currentTime = Random.Range(this.minScanPeriod, this.maxScanPeriod);
            }
        }

        public void OnStop(IBlackboard blackboard)
        {
            if (this.behaviour is IStopBehaviour behaviour)
            {
                behaviour.OnStop(blackboard);
            }
        }

        public void OnStart(IBlackboard blackboard)
        {
            if (this.behaviour is IStartBehaviour behaviour)
            {
                behaviour.OnStart(blackboard);
            }
        }
    }
}