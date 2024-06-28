using UnityEngine;
using UnityEngine.UI;

namespace SampleGame
{
    public sealed class PauseScreenView : MonoBehaviour
    {
        [SerializeField]
        public Button resumeGameButton;

        [SerializeField]
        public Button finishGameButton;

        [SerializeField]
        public Button exitAppButton;

        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}