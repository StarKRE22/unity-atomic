#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Contracts
{
    [CustomEditor(typeof(ValueCatalog))]
    internal sealed class ValueCatalogEditor : Editor
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
            ValueCatalog audioBank = this.target as ValueCatalog;
            IndexAPIGenerator.Generate(audioBank);
        }
    }
}
#endif