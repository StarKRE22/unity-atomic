#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Atomic.UI
{
    [InitializeOnLoad]
    public static class APICatalogService
    {
        private static UIAPICatalog catalog;
        
        static APICatalogService()
        {
            DebugUtils.ValueNameFormatter = ConvertIdToName;
        }
        
        internal static UIAPICatalog CreateCatalog()
        {
            string path = EditorUtility.SaveFilePanelInProject("Create View API Catalog", "ViewCatalog", "asset",
                "Please enter a file name to save the asset to");

            catalog = ScriptableObject.CreateInstance<UIAPICatalog>();

            AssetDatabase.CreateAsset(catalog, path);
            AssetDatabase.SaveAssets();

            try
            {
                AssetDatabase.Refresh();
            }
            catch (Exception)
            {
                // ignored
            }

            return catalog;
        }

        internal static UIAPICatalog GetCatalog()
        {
            if (catalog != null)
            {
                return catalog;
            }
        
            string[] guids = AssetDatabase.FindAssets("t:" + nameof(UIAPICatalog));
            int count = guids.Length;
            if (count == 0)
            {
                return null;
            }

            for (int i = 0; i < count; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
                UIAPICatalog apiCatalog = AssetDatabase.LoadAssetAtPath<UIAPICatalog>(assetPath);
                if (apiCatalog.Inactive)
                {
                    continue;
                }
                
                catalog = apiCatalog;
                return catalog;
            }

            return null;
        }

        private static string ConvertIdToName(int id)
        {
            UIAPICatalog catalog = GetCatalog();
            
            if (catalog == null)
            {
                return id.ToString();
            }
            
            return catalog.GetFullItemNameById(id);
        }
    }
}
#endif
