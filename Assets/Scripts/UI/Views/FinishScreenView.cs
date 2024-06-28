using UnityEngine;

namespace SampleGame
{
    public sealed class FinishScreenView : MonoBehaviour
    {
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