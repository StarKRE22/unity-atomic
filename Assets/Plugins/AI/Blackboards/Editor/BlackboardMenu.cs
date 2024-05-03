#if UNITY_EDITOR
using UnityEditor;

namespace AIModule.UnityEditor
{
    internal static class BlackboardMenu
    {
        [MenuItem("Window/AI/Blackboard", priority = 7)]
        internal static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(BlackboardWindow));
        }

        [MenuItem("Tools/AI/Select Blackboard Config", priority = 7)]
        internal static void SelectConfig()
        {
            Selection.activeObject = BlackboardConfig.EditorInstance;
        }
        
    }
}
#endif