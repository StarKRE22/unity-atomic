#if UNITY_EDITOR
using JetBrains.Annotations;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Atomic.Contexts
{
    [UsedImplicitly]
    internal sealed class ContextKeyAttributeDrawer : OdinAttributeDrawer<ContextKeyAttribute, int>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            APICatalog catalog = APICatalogService.GetCatalog();

            if (catalog == null)
            {
                GUILayout.Label(label);
                EditorGUILayout.HelpBox("No value catalog in project", MessageType.Error);
                return;
            }

            if (!catalog.HasCategories())
            {
                GUILayout.Label(label);
                EditorGUILayout.HelpBox("Categories is empty", MessageType.Error);
                return;
            }

            int itemId = this.ValueEntry.SmartValue;

            if (!catalog.FindCategoryAndItemById(itemId, out int categoryIndex, out int itemIndex))
            {
                categoryIndex = 0;
                itemIndex = 0;
            }

            Color prevColor = GUI.color;
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Id", GUILayout.Width(50));

            int newCategoryIndex = EditorGUILayout.Popup(categoryIndex, catalog.GetNotEmptyCategoryNames());
            if (newCategoryIndex != categoryIndex)
            {
                itemIndex = 0;
            }
            
            APICategory category = catalog.GetCategory(newCategoryIndex);
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
#endif