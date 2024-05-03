using UnityEngine;

namespace Game.Engine
{
    public sealed class Coin : MonoBehaviour
    {
        public void PickUp()
        {
            this.gameObject.SetActive(false);
        }
    }
}