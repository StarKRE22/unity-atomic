// using System;
// using UnityEditor;
// using UnityEngine;
//
// namespace AI.GOAP.UnityEditor
// {
//     [CustomPropertyDrawer(typeof(FactKeyAttribute))]
//     public sealed class FactKeyPropertyDrawler : PropertyDrawer
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
//             var idPosition = new Rect(position.x + 100, position.y, position.width - 100, position.height);
//
//             var id = property.stringValue;
//             var names = FactIdCatalog.GetIds();
//
//             if (string.IsNullOrEmpty(id))
//             {
//                 var currentIndex = 0;
//                 currentIndex = EditorGUI.Popup(idPosition, currentIndex, names);
//                 property.stringValue = names[currentIndex];
//             }
//             else if (Array.Exists(names, name => name == id))
//             {
//                 var currentIndex = Array.IndexOf(names, id);
//                 currentIndex = EditorGUI.Popup(idPosition, currentIndex, names);
//                 property.stringValue = names[currentIndex];
//             }
//             else
//             {
//                 EditorGUI.PropertyField(idPosition, property, GUIContent.none);
//             }
//
//             EditorGUI.EndProperty();
//         }
//     }
// }