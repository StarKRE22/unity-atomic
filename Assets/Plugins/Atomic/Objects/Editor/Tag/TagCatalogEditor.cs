using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Atomic.Objects
{
    [CustomEditor(typeof(TagCatalog))]
    internal sealed class TagCatalogEditor : OdinEditor
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
            GUI.color = new Color(0f, 0.83f, 1f, 1);
            if (GUILayout.Button("Compile"))
            {
                this.CompileKeys();
            }

            GUI.color = prevColor;
        }

        private void CompileKeys()
        {
            TagCatalog tagCatalog = this.target as TagCatalog;
            TagAPIGenerator.Generate(tagCatalog);
        }
    }
}