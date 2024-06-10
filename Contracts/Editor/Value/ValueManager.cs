#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Atomic.Contracts
{
    public static class ValueManager
    {
        private static ValueConfig _config;

        public static ValueConfig CreateValueConfig()
        {
            string path = EditorUtility.SaveFilePanelInProject("Create Value Catalog", "ValueCatalog", "asset",
                "Please enter a file name to save the asset to");

            // const string path = "Assets/Plugins/MonoObjects/Editor/ValueCatalog.asset";
            _config = ScriptableObject.CreateInstance<ValueConfig>();

            AssetDatabase.CreateAsset(_config, path);
            AssetDatabase.SaveAssets();

            try
            {
                AssetDatabase.Refresh();
            }
            catch (Exception)
            {
                // ignored
            }

            return _config;
        }

        public static ValueConfig GetValueConfig()
        {
            if (_config != null)
            {
                return _config;
            }
        
            string[] guids = AssetDatabase.FindAssets("t:" + nameof(ValueConfig));
            if (guids.Length == 0)
            {
                return null;
            }

            string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
            _config = AssetDatabase.LoadAssetAtPath<ValueConfig>(assetPath);
            return _config;
        }
    }
}
#endif


// const string path = "Assets/Plugins/MonoObjects/Editor/ValueCatalog.asset";
// ValueCatalog scriptableObject = AssetDatabase.LoadAssetAtPath<ValueCatalog>(path);
// return scriptableObject;