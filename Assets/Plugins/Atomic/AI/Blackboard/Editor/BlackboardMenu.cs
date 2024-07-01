#if UNITY_EDITOR
using UnityEditor;

namespace Modules.AI
{
    internal static class BlackboardMenu
    {
        [MenuItem("Window/Modules/AI/Blackboard", priority = 7)]
        internal static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(BlackboardWindow));
        }

        [MenuItem("Tools/Modules/AI/Select Blackboard Config", priority = 7)]
        internal static void SelectConfig()
        {
            Selection.activeObject = BlackboardConfig.EditorInstance;
        }
    }
}
#endif