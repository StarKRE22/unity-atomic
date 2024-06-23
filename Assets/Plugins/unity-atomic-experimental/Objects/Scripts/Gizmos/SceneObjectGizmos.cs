#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Objects
{
    ///Editor Only
    [AddComponentMenu("Atomic/Objects/Scene Object Gizmos")]
    [RequireComponent(typeof(SceneObject))]
    public sealed class SceneObjectGizmos : MonoBehaviour
    {
        [SerializeField]
        private bool drawGizmos = true;
        
        [SerializeField]
        private bool drawGizmosSelected = true;

#if ODIN_INSPECTOR
        [ShowIf(nameof(drawGizmos))]
#endif
        [Space]
        [SerializeReference]
        private IObjectGizmos[] gizmoses;

#if ODIN_INSPECTOR
        [ShowIf(nameof(drawGizmosSelected))]
#endif
        [SerializeReference]
        private IObjectGizmos[] gizmosesSelected;

        private SceneObject _sceneObject;
        
        // ReSharper disable once Unity.RedundantEventFunction
        private void Start()
        {
            //Required for enable check in Unity inspector :)
        }

        private void OnValidate()
        {
            _sceneObject = this.GetComponent<SceneObject>();
        }

        private void OnDrawGizmos()
        {
            if (EditorApplication.isPlaying)
            {
                return;
            }
            
            if (!this.enabled)
            {
                return;
            }
            
            if (!this.drawGizmos)
            {
                return;
            }

            if (this.gizmoses == null)
            {
                return;
            }

            if (_sceneObject == null)
            {
                _sceneObject = this.GetComponent<SceneObject>();
            }

            if (!EditorApplication.isPlaying)
            {
                _sceneObject.Install(Object.Dummy);
            }
            
            for (int i = 0, count = this.gizmoses.Length; i < count; i++)
            {
                try
                {
                    IObjectGizmos gizmos = this.gizmoses[i];
                    gizmos?.OnGizmosDraw(_sceneObject);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (!this.enabled)
            {
                return;
            }
            
            if (!this.drawGizmosSelected)
            {
                return;
            }

            if (this.gizmoses == null)
            {
                return;
            }

            if (_sceneObject == null)
            {
                _sceneObject = this.GetComponent<SceneObject>();
            }

            if (!EditorApplication.isPlaying)
            {
                IObject obj = Object.Dummy;
                _sceneObject.Install(obj);
            }
            
            for (int i = 0, count = this.gizmosesSelected.Length; i < count; i++)
            {
                try
                {
                    IObjectGizmos gizmos = this.gizmosesSelected[i];
                    gizmos?.OnGizmosDraw(_sceneObject);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
    }
}
#endif