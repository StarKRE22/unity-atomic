using System;
using AIModule;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class BTNode_NextWaypoint : BTNode
    {
        public override string Name => "Next Waypoint";

        [SerializeField, BlackboardKey]
        private ushort waypoints;

        [SerializeField, BlackboardKey]
        private ushort waypointIndex;

        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetObject(this.waypoints, out Transform[] waypoints) ||
                !blackboard.TryGetInt(this.waypointIndex, out int waypointIndex))
            {
                return BTState.FAILURE;
            }

            waypointIndex++;
            waypointIndex %= waypoints.Length;

            blackboard.SetInt(this.waypointIndex, waypointIndex);
            return BTState.SUCCESS;
        }
    }
}