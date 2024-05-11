using UnityEngine;

namespace Sample
{
    public sealed class PlayerWallet : MonoBehaviour
    {
        [field: SerializeField]
        public int Money { get; private set; }

        public void EarnMoney(int range)
        {
            this.Money += range;
        }
    }
}