#if UNITY_EDITOR
using System;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Modules.AI
{
    [MovedFrom(true, null, null, "WireDiskGizmos")]
    [Serializable]
    public sealed class WireDiskGizmos : IDrawGizmos
    {
        [BlackboardKey]
        [SerializeField]
        private int centerKey;

        [BlackboardKey]
        [SerializeField]
        private int radiusKey;

        [BlackboardKey]
        [SerializeField]
        private int normalKey;

        [SerializeField]
        private Color color = Color.magenta;

        [SerializeField]
        private float thikness = 0.1f;
        
        public void OnGizmos(IBlackboard blackboard)
        {
            if (!blackboard.TryGetObject(this.centerKey, out Transform center) ||
                !blackboard.TryGetFloat(this.radiusKey, out float radius) ||
                !blackboard.TryGetFloat3(this.normalKey, out float3 normal))
            {
                return;
            }

            Color prevColor = Handles.color;
            Handles.color = this.color;
            Handles.DrawWireDisc(center.position, normal, radius, this.thikness);
            Handles.color = prevColor;
        }
    }
}
#endif