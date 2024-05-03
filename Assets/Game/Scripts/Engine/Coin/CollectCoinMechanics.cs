using Atomic;
using UnityEngine;

namespace Game.Engine
{
    public sealed class CollectCoinMechanics
    {
        private readonly IAtomicEvent coinCollectEvent;

        public CollectCoinMechanics(IAtomicEvent coinCollectEvent)
        {
            this.coinCollectEvent = coinCollectEvent;
        }

        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent(out Coin coin))
            {
                coin.PickUp();
                this.coinCollectEvent.Invoke();
            }
        }
    }
}