#if UNITY_EDITOR
using UnityEditor;

namespace Atomic.UI
{
    internal static class MenuItems
    {
        [MenuItem("Window/Atomic/View Window", priority = 7)]
        internal static void ShowValueCatalogWindow()
        {
            EditorWindow.GetWindow(typeof(APICatalogWindow));
        }

        [MenuItem("Tools/Atomic/View Catalog", priority = 7)]
        internal static void SelectValueCatalog()
        {
            Selection.activeObject = APICatalogService.GetCatalog();
            
            if (Selection.activeObject == null)
            {
                Selection.activeObject = APICatalogService.CreateCatalog();
            }
        }
    }
}
#endif