using Atomic;
using Atomic.Objects;
using GameEngine;

namespace Sample
{
    public class CollectCoinMechanicsRx : IAtomicObject.IComposable
    {
        public void Compose(IAtomicObject obj)
        {
            obj.SubscribeOnTriggerEnter2D((_, collider) =>
            {
                if (collider.TryGetComponent(out Coin coin))
                {
                    coin.PickUp();
                    obj.InvokeAction(CommonAPI.CollectCoinEvent);
                }
            });
        }
    }
}