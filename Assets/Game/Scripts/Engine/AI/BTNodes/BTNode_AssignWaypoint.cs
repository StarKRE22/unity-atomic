using System;
using AIModule;
using UnityEngine;

namespace Game.Engine
{
    [Serializable]
    public sealed class BTNode_AssignWaypoint : BTNode
    {
        public override string Name => "Assign Waypoint";

        [SerializeField, BlackboardKey]
        private ushort waypoints;

        [SerializeField, BlackboardKey]
        private ushort waypointIndex;

        [SerializeField, BlackboardKey]
        private ushort movePosition;

        protected override BTState OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetObject(this.waypoints, out Transform[] waypoints) ||
                !blackboard.TryGetInt(this.waypointIndex, out int waypointIndex))
            {
                return BTState.FAILURE;
            }

            Vector2 movePosition = waypoints[waypointIndex].position;
            blackboard.SetVector2(this.movePosition, movePosition);
            return BTState.SUCCESS;
        }
    }
}