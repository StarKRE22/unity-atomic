using JetBrains.Annotations;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Atomic.Objects
{
    [UsedImplicitly]
    public sealed class ReferenceIdAttributeDrawer : OdinAttributeDrawer<ValueIdAttribute, int>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            ValueCatalog catalog = CatalogTools.GetReferenceCatalog();

            if (catalog == null)
            {
                GUILayout.Label(label);
                EditorGUILayout.HelpBox("No value catalog in project", MessageType.Error);
                return;
            }

            if (catalog.categories.Count <= 0)
            {
                GUILayout.Label(label);
                EditorGUILayout.HelpBox("Categories is empty", MessageType.Error);
                return;
            }

            int itemId = this.ValueEntry.SmartValue;

            if (!catalog.FindCategoryAndItemById(itemId, out var categoryIndex, out int itemIndex))
            {
                categoryIndex = 0;
                itemIndex = 0;
            }

            Color prevColor = GUI.color;

            // GUI.color = new Color(0f, 0.83f, 1f, 1);
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Id", GUILayout.Width(50));

            var newCategoryIndex = EditorGUILayout.Popup(categoryIndex, catalog.GetCategoryNamesNotEmpty());
            if (newCategoryIndex != categoryIndex)
            {
                itemIndex = 0;
            }
            
            var category = catalog.categories[newCategoryIndex];
            if (category.IsEmpty())
            {
                GUI.color = Color.yellow;
                EditorGUILayout.LabelField("   Category is empty!", GUILayout.ExpandWidth(true));
                this.ValueEntry.SmartValue = -1;
                GUI.color = prevColor;
            }
            else
            {
                itemIndex = EditorGUILayout.Popup(GUIContent.none, itemIndex, category.GetItemNamesWithIds());
                this.ValueEntry.SmartValue = category.GetItem(itemIndex).id;
            }

            EditorGUILayout.EndHorizontal();
            GUI.color = prevColor;
        }
    }
}