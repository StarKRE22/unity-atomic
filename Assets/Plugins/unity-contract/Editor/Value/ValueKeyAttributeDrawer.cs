using JetBrains.Annotations;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Contracts
{
    [UsedImplicitly]
    public sealed class ValueKeyAttributeDrawer : OdinAttributeDrawer<ContractValueAttribute, string>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            ValueCatalog catalog = EditorTools.GetValueCatalog();

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

            string valueKey = this.ValueEntry.SmartValue; //HealthAPI.HitPoints

            RetrieveIndexes(valueKey, catalog, out int categoryIndex, out int itemIndex);

            Color prevColor = GUI.color;

            // GUI.color = new Color(0f, 0.83f, 1f, 1);
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Key ->", GUILayout.Width(50));

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
                this.ValueEntry.SmartValue = $"{category.name}{catalog.suffix}";
                GUI.color = prevColor;
            }
            else
            {
                itemIndex = EditorGUILayout.Popup(GUIContent.none, itemIndex, category.GetItemNames());
                this.ValueEntry.SmartValue = $"{category.name}{catalog.suffix}.{category.GetItem(itemIndex).name}";
            }

            EditorGUILayout.EndHorizontal();
            GUI.color = prevColor;
        }

        private static void RetrieveIndexes(string input, ValueCatalog catalog, out int categoryIndex,
            out int itemIndex)
        {
            string[] chunks = input.Split('.');

            if (chunks.Length is not (1 or 2))
            {
                categoryIndex = 0;
                itemIndex = 0;
                return;
            }
            
            string categoryName = chunks[0];
            if (string.IsNullOrEmpty(categoryName))
            {
                categoryIndex = 0;
                itemIndex = 0;
                return;
            }
            
            string itemName;
            
            if (chunks.Length == 1)
            {
                itemName = "";
                itemIndex = 0;
            }
            else
            {
                itemName = chunks[1];
            }

            if (categoryName!.EndsWith(catalog.suffix))
            {
                categoryName = categoryName[..^catalog.suffix.Length];
            }
            
            categoryIndex = catalog.IndexOfCategory(categoryName, out var category);
            if (categoryIndex == -1)
            {
                categoryIndex = 0;
            }
            
            if (string.IsNullOrEmpty(itemName))
            {
                itemIndex = 0;
                return;
            }
            
            itemIndex = category.IndexOfItem(itemName);
            if (itemIndex == -1)
            {
                itemIndex = 0;
            }
        }
    }
}