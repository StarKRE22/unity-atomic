#if UNITY_EDITOR
using System;
using UnityEngine;

namespace Modules.AI
{
    [AddComponentMenu("Modules/AI/AI Gizmos Drawer")]
    [DisallowMultipleComponent]
    public sealed class SceneGizmosDrawer : MonoBehaviour
    {
        [SerializeField]
        private SceneBlackboard blackboard;

        [SerializeReference]
        private IDrawGizmos[] gizmoses;

        [SerializeField]
        private bool drawGizmos;

        [SerializeField]
        private bool drawSelected;

        private void Start()
        {
            //For checkbox in inspector...
        }

        private void OnDrawGizmos()
        {
            if (!this.enabled)
            {
                return;
            }
            
            if (!this.drawGizmos)
            {
                return;
            }

            if (this.blackboard == null)
            {
                return;
            }

            if (this.gizmoses == null)
            {
                return;
            }

            try
            {
                for (int i = 0, count = this.gizmoses.Length; i < count; i++)
                {
                    IDrawGizmos logic = this.gizmoses[i];
                    logic?.OnGizmos(this.blackboard);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (!this.enabled)
            {
                return;
            }
            
            if (!this.drawSelected)
            {
                return;
            }

            if (this.blackboard == null)
            {
                return;
            }

            if (this.gizmoses == null)
            {
                return;
            }

            try
            {
                for (int i = 0, count = this.gizmoses.Length; i < count; i++)
                {
                    IDrawGizmos logic = this.gizmoses[i];
                    logic?.OnGizmos(this.blackboard);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
#endif