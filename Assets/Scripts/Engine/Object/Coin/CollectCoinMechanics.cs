using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Sample
{
    [Serializable]
    public sealed class CollectCoinMechanics : ITriggerEnter2D
    {
        private IAtomicEvent coinCollectEvent;

        public CollectCoinMechanics()
        {
        }

        public void Compose(IAtomicEvent coinCollectEvent)
        {
            this.coinCollectEvent = coinCollectEvent;
        }

        public CollectCoinMechanics(IAtomicEvent coinCollectEvent)
        {
            this.coinCollectEvent = coinCollectEvent;
        }

        public void TriggerEnter2D(IAtomicObject obj, Collider2D collider)
        {
            if (collider.TryGetComponent(out Coin coin))
            {
                coin.PickUp();
                this.coinCollectEvent.Invoke();
            }
        }
    }
}