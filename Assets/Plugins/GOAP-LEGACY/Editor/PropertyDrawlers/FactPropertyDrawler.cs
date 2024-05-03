// #if UNITY_EDITOR
//
// using System;
// using UnityEditor;
// using UnityEngine;
//
// namespace AI.GOAP.UnityEditor
// {
//     [CustomPropertyDrawer(typeof(Fact))]
//     public sealed class FactPropertyDrawler : PropertyDrawer
//     {
//         public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//         {
//             EditorGUI.BeginProperty(position, label, property);
//
//             // Рисуем заголовок:
//             var labelPosition = new Rect(position.x, position.y, 100, position.height);
//             EditorGUI.LabelField(labelPosition, label.text);
//
//             // Рисуем id:
//             var idPosition = new Rect(position.x + 100, position.y, position.width / 2, position.height);
//
//             var serializedId = property.FindPropertyRelative("id");
//             var id = serializedId.stringValue;
//             var names = FactIdCatalog.GetIds();
//
//             if (string.IsNullOrEmpty(id))
//             {
//                 var currentIndex = 0;
//                 currentIndex = EditorGUI.Popup(idPosition, currentIndex, names);
//                 serializedId.stringValue = names[currentIndex];
//             }
//             else if (Array.Exists(names, name => name == id))
//             {
//                 var currentIndex = Array.IndexOf(names, id);
//                 currentIndex = EditorGUI.Popup(idPosition, currentIndex, names);
//                 serializedId.stringValue = names[currentIndex];
//             }
//             else
//             {
//                 EditorGUI.PropertyField(idPosition, serializedId, GUIContent.none);
//             }
//
//             // Рисуем value:
//             var valuePosition = new Rect(position.x + position.width - 15, position.y, 10, position.height);
//             var serializedValue = property.FindPropertyRelative("value");
//             var prevColor = GUI.backgroundColor;
//             var colorHTML = serializedValue.boolValue ? "#B1EEF1" : "#FFDBBB";
//             ColorUtility.TryParseHtmlString(colorHTML, out var valueColor);
//
//             GUI.backgroundColor = valueColor;
//             EditorGUI.PropertyField(valuePosition, serializedValue, GUIContent.none);
//             GUI.backgroundColor = prevColor;
//
//             EditorGUI.EndProperty();
//         }
//     }
// }
// #endif