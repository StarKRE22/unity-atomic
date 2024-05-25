using Atomic;
using Atomic.Objects;
using GameEngine;

namespace Sample
{
    public class CollectCoinMechanicsRx : IAtomicAspect
    {
        public void Compose(IAtomicObject obj)
        {
            obj.SubscribeOnTriggerEnter2D((_, collider) =>
            {
                if (collider.TryGetComponent(out Coin coin))
                {
                    coin.PickUp();
                    obj.InvokeAtomicAction(CommonAPI.CollectCoinEvent);
                }
            });
        }
    }
}