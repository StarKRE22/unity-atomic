#if UNITY_EDITOR
using UnityEditor;

namespace Atomic.Contracts
{
    public static class TagMenu
    {
        [MenuItem("Window/AtomicObjects/Tag Window", priority = 7)]
        internal static void ShowTagCatalogWindow()
        {
            EditorWindow.GetWindow(typeof(TagWindow));
        }
        
        [MenuItem("Tools/AtomicObjects/Select Tag Catalog", priority = 7)]
        internal static void SelectTagCatalog()
        {
            Selection.activeObject = TagManager.GetTagConfig();
        }
    }
}
#endif