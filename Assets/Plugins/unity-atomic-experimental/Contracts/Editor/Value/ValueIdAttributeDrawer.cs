using JetBrains.Annotations;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using Atomic.Objects;

namespace Atomic.Contracts
{
    [UsedImplicitly]
    public sealed class ValueIdAttributeDrawer : OdinAttributeDrawer<ValueContractAttribute, int>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            ValueConfig config = ValueManager.GetValueConfig();

            if (config == null)
            {
                GUILayout.Label(label);
                EditorGUILayout.HelpBox("No value catalog in project", MessageType.Error);
                return;
            }

            if (config.categories.Count <= 0)
            {
                GUILayout.Label(label);
                EditorGUILayout.HelpBox("Categories is empty", MessageType.Error);
                return;
            }

            int itemId = this.ValueEntry.SmartValue;

            if (!config.FindCategoryAndItemById(itemId, out var categoryIndex, out int itemIndex))
            {
                categoryIndex = 0;
                itemIndex = 0;
            }

            Color prevColor = GUI.color;

            // GUI.color = new Color(0f, 0.83f, 1f, 1);
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Id", GUILayout.Width(50));

            var newCategoryIndex = EditorGUILayout.Popup(categoryIndex, config.GetCategoryNamesNotEmpty());
            if (newCategoryIndex != categoryIndex)
            {
                itemIndex = 0;
            }
            
            var category = config.categories[newCategoryIndex];
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