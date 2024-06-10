#if UNITY_EDITOR
using UnityEditor;

namespace Atomic.Contracts
{
    internal static class MenuItems
    {
        [MenuItem("Window/AtomicObjects/Value Window", priority = 7)]
        internal static void ShowValueCatalogWindow()
        {
            EditorWindow.GetWindow(typeof(ValueWindow));
        }

        [MenuItem("Tools/AtomicObjects/Select Value Catalog", priority = 7)]
        internal static void SelectValueCatalog()
        {
            Selection.activeObject = ValueManager.GetValueConfig();
            if (Selection.activeObject == null)
            {
                Selection.activeObject = ValueManager.CreateValueConfig();
            }
        }
    }
}
#endif