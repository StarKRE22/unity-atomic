using Atomic;
using Atomic.Objects;
using GameEngine;

namespace Sample
{
    public sealed class CollectCoinObserver
    {
        private const int MONEY_RANGE = 1;
        
        private readonly IAtomicObject target;
        private readonly PlayerWallet playerWallet;

        public CollectCoinObserver(IAtomicObject target, PlayerWallet playerWallet)
        {
            this.target = target;
            this.playerWallet = playerWallet;
        }

        public void OnEnable()
        {
            this.target.SubscribeOnAtomicEvent(CommonAPI.CollectCoinEvent, this.OnCoinCollected);
        }

        public void OnDisable()
        {
            this.target.UnsubscribeFromAtomicEvent(CommonAPI.CollectCoinEvent, this.OnCoinCollected);
        }

        private void OnCoinCollected()
        {
            this.playerWallet.EarnMoney(MONEY_RANGE);
        }
    }
}