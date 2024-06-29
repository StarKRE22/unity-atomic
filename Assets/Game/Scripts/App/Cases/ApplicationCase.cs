using UnityEditor;

namespace SampleGame.App
{
    public static class ApplicationCase
    {
        public static void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit(0);
#endif
        }
    }
}