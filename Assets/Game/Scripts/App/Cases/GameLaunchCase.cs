using UnityEngine.SceneManagement;

namespace SampleGame.App
{
    public static class GameLaunchCase
    {
        public static void LaunchGame()
        {
            SceneManager.LoadScene("Game");
        }
    }
}