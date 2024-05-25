#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Atomic.Objects
{
    public static class CatalogTools
    {
        public static TagCatalog GetTagCatalog()
        {
            const string path = "Assets/Plugins/Atomic/Objects/Editor/TagCatalog.asset";
            TagCatalog scriptableObject = AssetDatabase.LoadAssetAtPath<TagCatalog>(path);
            return scriptableObject;
        }

        public static void CreateTagCatalog()
        {
            const string path = "Assets/Plugins/Atomic/Objects/Editor/TagCatalog.asset";
            var scriptableObject = ScriptableObject.CreateInstance<TagCatalog>();
            
            AssetDatabase.CreateAsset(scriptableObject, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        public static void CreateReferenceCatalog()
        {
            const string path = "Assets/Plugins/Atomic/Objects/Editor/ReferenceCatalog.asset";
            var scriptableObject = ScriptableObject.CreateInstance<ValueCatalog>();
            
            AssetDatabase.CreateAsset(scriptableObject, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        public static ValueCatalog GetReferenceCatalog()
        {
            const string path = "Assets/Plugins/Atomic/Objects/Editor/ReferenceCatalog.asset";
            ValueCatalog scriptableObject = AssetDatabase.LoadAssetAtPath<ValueCatalog>(path);
            return scriptableObject;
        }
    }
}
#endif