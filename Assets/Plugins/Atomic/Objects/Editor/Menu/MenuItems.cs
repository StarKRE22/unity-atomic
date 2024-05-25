#if UNITY_EDITOR
using UnityEditor;

namespace Atomic.Objects
{
    internal static class MenuItems
    {
        [MenuItem("Window/Atomic Objects/Value Window", priority = 7)]
        internal static void ShowValueCatalogWindow()
        {
            EditorWindow.GetWindow(typeof(ValueWindow));
        }
        
        [MenuItem("Window/Atomic Objects/Tag Window", priority = 7)]
        internal static void ShowTagCatalogWindow()
        {
            EditorWindow.GetWindow(typeof(TagWindow));
        }

        [MenuItem("Assets/Atomic Objects/Select Value Catalog", priority = 7)]
        internal static void SelectReferenceCatalog()
        {
            Selection.activeObject = CatalogTools.GetReferenceCatalog();
        }
        
        [MenuItem("Assets/Atomic Objects/Select Tag Catalog", priority = 7)]
        internal static void SelectTagCatalog()
        {
            Selection.activeObject = CatalogTools.GetTagCatalog();
        }
    }
}
#endif