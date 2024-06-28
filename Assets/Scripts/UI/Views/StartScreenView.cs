using UnityEngine;
using UnityEngine.UI;

namespace SampleGame
{
    public sealed class StartScreenView : MonoBehaviour
    {
        [SerializeField]
        public Button startButton;
        
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