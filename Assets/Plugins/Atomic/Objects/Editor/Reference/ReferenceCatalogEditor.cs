#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Atomic.Objects
{
    [CustomEditor(typeof(ReferenceCatalog))]
    internal sealed class ReferenceCatalogEditor : OdinEditor
    {
        public override void OnInspectorGUI()
        {
            this.DrawCompileButton();
            GUILayout.Space(8);
            base.OnInspectorGUI();
        }

        private void DrawCompileButton()
        {
            Color prevColor = GUI.color;
            GUI.color = new Color(0f, 0.83f, 1f);
            if (GUILayout.Button("Compile"))
            {
                this.CompileKeys();
            }

            GUI.color = prevColor;
        }

        private void CompileKeys()
        {
            ReferenceCatalog audioBank = this.target as ReferenceCatalog;
            ReferenceAPIGenerator.Generate(audioBank);
        }
    }
}
#endif