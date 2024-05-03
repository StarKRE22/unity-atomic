using UnityEditor;
using UnityEngine;

namespace Contracts
{
    internal static class EditorTools
    {
        public static TypeCatalog GetTypeCatalog()
        {
            const string path = "Assets/Plugins/unity-contract/TypeCatalog.asset";
            TypeCatalog scriptableObject = AssetDatabase.LoadAssetAtPath<TypeCatalog>(path);
            return scriptableObject;
        }

        public static void CreateTypeCatalog()
        {
            const string path = "Assets/Plugins/unity-contract/TypeCatalog.asset";
            var scriptableObject = ScriptableObject.CreateInstance<TypeCatalog>();
            
            AssetDatabase.CreateAsset(scriptableObject, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        public static void CreateValueCatalog()
        {
            const string path = "Assets/Plugins/unity-contract/ValueCatalog.asset";
            var scriptableObject = ScriptableObject.CreateInstance<ValueCatalog>();
            
            AssetDatabase.CreateAsset(scriptableObject, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        internal static ValueCatalog GetValueCatalog()
        {
            const string path = "Assets/Plugins/unity-contract/ValueCatalog.asset";
            ValueCatalog scriptableObject = AssetDatabase.LoadAssetAtPath<ValueCatalog>(path);
            return scriptableObject;
        }
    }
}