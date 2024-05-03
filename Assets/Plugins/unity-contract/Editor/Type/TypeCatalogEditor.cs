using UnityEditor;
using UnityEngine;

namespace Contracts
{
    [CustomEditor(typeof(TypeCatalog))]
    internal sealed class TypeCatalogEditor : Editor
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
            TypeCatalog typeCatalog = this.target as TypeCatalog;
            TypeAPIGenerator.Generate(typeCatalog);
        }
    }
}