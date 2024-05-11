// #if UNITY_EDITOR
// using JetBrains.Annotations;
// using Sirenix.OdinInspector.Editor;
// using Sirenix.Utilities.Editor;
// using UnityEditor;
// using UnityEngine;
//
// namespace Atomic.Objects
// {
//     [UsedImplicitly]
//     public sealed class TypeKeyAttributeDrawler : OdinAttributeDrawer<TagIdAttribute, string>
//     {
//         protected override void DrawPropertyLayout(GUIContent label)
//         {
//             TagCatalog catalog = EditorTools.GetTypeCatalog();
//             
//             if (catalog == null)
//             {
//                 GUILayout.Label(label);
//                 EditorGUILayout.HelpBox("No type catalog in project", MessageType.Error);
//                 return;
//             }
//
//             if (catalog.items.Count <= 0)
//             {
//                 GUILayout.Label(label);
//                 EditorGUILayout.HelpBox("Type catalog is empty", MessageType.Error);
//                 return;
//             }
//
//             string typeKey = this.ValueEntry.SmartValue;
//
//             int index = catalog.IndexOfItem(typeKey);
//             if (index == -1 || string.IsNullOrEmpty(typeKey))
//             {
//                 index = 0;
//             }
//
//             Color prevColor = GUI.color;
//             
//             // GUI.color = new Color(0f, 0.83f, 1f, 1);
//             // GUIHelper.PushLabelWidth(GUIHelper.BetterLabelWidth);
//
//             EditorGUILayout.BeginHorizontal();
//             EditorGUILayout.LabelField("Key ->", GUILayout.Width(70));
//             
//             index = EditorGUILayout.Popup(GUIContent.none, index, catalog.GetItemTypes());
//             this.ValueEntry.SmartValue = catalog.GetItem(index).type;
//             
//             // GUIHelper.PopLabelWidth();
//             EditorGUILayout.EndHorizontal();
//             GUI.color = prevColor;
//         }
//     }
// }
// #endif