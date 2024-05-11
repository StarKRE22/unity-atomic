using UnityEngine;

namespace Sample
{
    public sealed class Coin : MonoBehaviour
    {
        public void PickUp()
        {
            this.gameObject.SetActive(false);
        }
    }
}