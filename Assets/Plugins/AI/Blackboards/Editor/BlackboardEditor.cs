#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AIModule.UnityEditor
{
    [CustomEditor(typeof(Blackboard))]
    public sealed class BlackboardEditor : OdinEditor
    {
        private Blackboard blackboard;
        private BlackboardConfig blackboardConfig;

        private void Awake()
        {
            this.blackboard = (Blackboard) this.target;
            this.blackboardConfig = BlackboardConfig.EditorInstance;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (EditorApplication.isPlaying)
            {
                EditorGUILayout.Space(8);
                SirenixEditorGUI.DrawThickHorizontalSeparator();
                this.DrawBlackboardState();
            }
        }

        private void DrawBlackboardState()
        {
            GUI.enabled = false;

            var variables = new Dictionary<string, object>();

            //Можно отрисовывать сразу значения!!!
            foreach ((ushort key, bool value) in this.blackboard.BoolValues())
            {
                string name = this.blackboardConfig.NameOf(key);
                variables[name] = value;
            }

            foreach ((ushort key, int value) in this.blackboard.IntValues())
            {
                string name = this.blackboardConfig.NameOf(key);
                variables[name] = value;
            }

            foreach ((ushort key, float value) in this.blackboard.FloatValues())
            {
                string name = this.blackboardConfig.NameOf(key);
                variables[name] = value;
            }

            foreach ((ushort key, object value) in this.blackboard.ObjectValues())
            {
                string name = this.blackboardConfig.NameOf(key);
                variables[name] = value;
            }

            foreach ((ushort key, object value) in this.blackboard.Vector2Values())
            {
                string name = this.blackboardConfig.NameOf(key);
                variables[name] = value;
            }

            foreach ((ushort key, object value) in this.blackboard.Vector3Values())
            {
                string name = this.blackboardConfig.NameOf(key);
                variables[name] = value;
            }

            foreach ((ushort key, object value) in this.blackboard.QuaternionValues())
            {
                string name = this.blackboardConfig.NameOf(key);
                variables[name] = value;
            }

            foreach (var variable in variables)
            {
                this.DrawVariable(variable);
            }

            GUI.enabled = true;
        }

        private void DrawVariable(KeyValuePair<string, object> variable)
        {
            (string name, object value) = variable;

            if (value is int intValue)
            {
                EditorGUILayout.IntField(name, intValue);
            }
            else if (value is float floatValue)
            {
                EditorGUILayout.FloatField(name, floatValue);
            }
            else if (value is bool booleValue)
            {
                EditorGUILayout.Toggle(name, booleValue);
            }
            else if (value is string stringValue)
            {
                EditorGUILayout.TextField(name, stringValue);
            }
            else if (value is Object unityObject)
            {
                EditorGUILayout.ObjectField(name, unityObject, typeof(Object), allowSceneObjects: true);
            }
            // else if (value is IEnumerable enumerable)
            // {
            //     EditorGUI.indentLevel++;
            //     foreach (var e in enumerable)
            //     {
            //         
            //     }
            //     EditorGUI.indentLevel--;
            // }


            else if (value is Vector2 vector2)
            {
                EditorGUILayout.Vector2Field(name, vector2);
            }
            else if (value is Vector3 vector3)
            {
                EditorGUILayout.Vector3Field(name, vector3);
            }
            else if (value is Quaternion quaternion)
            {
                EditorGUILayout.Vector3Field(name, quaternion.eulerAngles);
            }
            else if (value is Enum enumValue)
            {
                EditorGUILayout.EnumPopup(enumValue);
            }
            else
            {
                EditorGUILayout.TextField(name, value != null ? value.ToString() : "null");
            }
        }
    }
}
#endif