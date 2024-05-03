#if UNITY_EDITOR
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace AIModule.UnityEditor
{
    public sealed class BlackboardWindow : EditorWindow
    {
        private const string NEW_KEY_NAME = "Name";
        private const string NEW_KEY_TYPE = "Type";

        private BlackboardConfig config;

        private SerializedProperty keys;

        private SerializedObject serializedConfig;

        private Vector2 scrollPosition;

        private string _newKeyName = NEW_KEY_NAME;
        private string _newKeyType = NEW_KEY_TYPE;

        private void OnEnable()
        {
            this.config = BlackboardConfig.EditorInstance;
            this.DrawTitle();
        }

        private void DrawTitle()
        {
            this.titleContent = new GUIContent("Blackboard");
        }

        private void OnGUI()
        {
            if (this.config.HasDuplicateIds(out ushort id))
            {
                EditorGUILayout.HelpBox($"There are duplicate id! Please, fix id: {id}!", MessageType.Error);
            }

            if (this.config.HasDuplicateNames(out string key))
            {
                EditorGUILayout.HelpBox($"There are duplicate keys! Please, fix key: {key}!", MessageType.Warning);
            }

            this.config.Sort();
            this.serializedConfig = new SerializedObject(this.config);
            this.keys = this.serializedConfig.FindProperty(nameof(this.keys));

            EditorGUILayout.Space(8);

            EditorGUILayout.BeginVertical();
            this.scrollPosition = EditorGUILayout.BeginScrollView(
                this.scrollPosition,
                GUILayout.ExpandWidth(true),
                GUILayout.ExpandHeight(true)
            );

            EditorGUI.BeginChangeCheck();

            GUI.enabled = false;
            EditorGUILayout.TextField(new GUIContent("Key: 0"), "UNDEFINED");
            GUI.enabled = true;

            for (int i = 1, count = this.keys.arraySize; i < count; i++)
            {
                this.DrawKey(i);
            }

            EditorGUILayout.Space(4);

            this.DrawAddButton();

            EditorGUILayout.Space(16);

            this.DrawCompileButton();

            if (EditorGUI.EndChangeCheck())
            {
                this.serializedConfig.ApplyModifiedProperties();
            }

            EditorGUILayout.EndScrollView();


            EditorGUILayout.EndVertical();
        }


        private void DrawAddButton()
        {
            var prevColor = GUI.color;

            const string namePattern = @"^[a-zA-Z_][a-zA-Z0-9_]*$";
            
            if (Regex.IsMatch(_newKeyName, namePattern) && !this.config.HasKeyWithName(_newKeyName))
            {
                GUI.color = Color.yellow;

                EditorGUILayout.BeginHorizontal();

                _newKeyName = EditorGUILayout.TextField(new GUIContent($"Key: {this.config.GetFreeId()}"), _newKeyName);
                _newKeyType = EditorGUILayout.TextField(_newKeyType, GUILayout.Width(200));

                if (GUILayout.Button("+", GUILayout.Width(50)))
                {
                    this.config.NewKey(_newKeyName, _newKeyType);
                    _newKeyName = NEW_KEY_NAME;
                    _newKeyType = NEW_KEY_TYPE;
                }

                EditorGUILayout.EndHorizontal();
            }
            else
            {
                GUI.color = Color.red;

                EditorGUILayout.BeginHorizontal();

                _newKeyName = EditorGUILayout.TextField(new GUIContent($"Key: {this.config.GetFreeId()}"), _newKeyName);

                GUI.enabled = false;
                _newKeyType = EditorGUILayout.TextField(_newKeyType, GUILayout.Width(200));

                GUILayout.Button("+", GUILayout.Width(50));
                GUI.enabled = true;

                EditorGUILayout.EndHorizontal();
            }

            GUI.color = prevColor;
        }


        private void DrawCompileButton()
        {
            var prevColor = GUI.color;
            GUI.color = Color.green;

            if (GUILayout.Button("Compile", GUILayout.Height(30)))
            {
                BlackboardAPIGenerator.Generate(this.config.Keys);
            }

            GUI.color = prevColor;
        }

        private void DrawKey(int index)
        {
            SerializedProperty key = this.keys.GetArrayElementAtIndex(index);
            SerializedProperty id = key.FindPropertyRelative(nameof(BlackboardConfig.Key.id));
            SerializedProperty name = key.FindPropertyRelative(nameof(BlackboardConfig.Key.name));
            SerializedProperty type = key.FindPropertyRelative(nameof(BlackboardConfig.Key.type));

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(name, new GUIContent($"Key: {id.intValue}"), GUILayout.ExpandWidth(true));
            type.stringValue = EditorGUILayout.TextField(type.stringValue, GUILayout.Width(200));

            var prevColor = GUI.color;
            GUI.color = Color.red;

            if (GUILayout.Button("-", GUILayout.Width(50)))
            {
                this.config.RemoveAt(index);
            }

            GUI.color = prevColor;

            EditorGUILayout.EndHorizontal();
        }
    }
}
#endif