#if UNITY_EDITOR
using System;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Modules.AI
{
    [MovedFrom(true, null, null, "WireCubeAIGizmos")] 
    [Serializable]
    public sealed class WireCubeGizmos : IDrawGizmos
    {
        [BlackboardKey]
        [SerializeField]
        private int centerKey;

        [BlackboardKey]
        [SerializeField]
        private int boxSizeKey;

        [SerializeField]
        private Color color = Color.magenta;
        
        public void OnGizmos(IBlackboard blackboard)
        {
            if (!blackboard.TryGetObject(this.centerKey, out Transform center) ||
                !blackboard.TryGetFloat3(this.boxSizeKey, out float3 boxSize))
            {
                return;
            }

            Color prevColor = Handles.color;
            Handles.color = this.color;
            Handles.DrawWireCube(center.position, boxSize);
            Handles.color = prevColor;
        }
    }
}
#endif