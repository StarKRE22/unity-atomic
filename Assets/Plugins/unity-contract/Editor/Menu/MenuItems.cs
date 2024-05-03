#if UNITY_EDITOR
using UnityEditor;

namespace Contracts
{
    internal static class MenuItems
    {
        [MenuItem("Window/Contracts/Value Catalog Window", priority = 7)]
        internal static void ShowIndexCatalogWindow()
        {
            EditorWindow.GetWindow(typeof(ValueCatalogWindow));
        }
        
        [MenuItem("Window/Contracts/Type Catalog Window", priority = 7)]
        internal static void ShowTypeCatalogWindow()
        {
            EditorWindow.GetWindow(typeof(TypeCatalogWindow));
        }

        [MenuItem("Assets/Contracts/Select Value Catalog", priority = 7)]
        internal static void SelectIndexCatalog()
        {
            Selection.activeObject = EditorTools.GetValueCatalog();
        }
        
        [MenuItem("Assets/Contracts/Select Type Catalog", priority = 7)]
        internal static void SelectTypeCatalog()
        {
            Selection.activeObject = EditorTools.GetTypeCatalog();
        }
    }
}
#endif