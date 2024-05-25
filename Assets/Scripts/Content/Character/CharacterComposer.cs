using System;
using Atomic.Elements;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Sample
{
    public sealed class CharacterComposer : MonoComposer
    {
        [SerializeField]
        private AtomicVariable<int> health;

        [SerializeField]
        private AtomicVariable<float> speed;

        public override void Compose(IAtomicObject obj)
        {
            obj.AddValue(CommonAPI.Health, this.health);
            obj.AddValue(CommonAPI.Speed, this.speed);
            obj.AddLogic<FlipMechanicsDynamic>();
            
            //
            // obj.SubcribeOnFixedUpdate((_, deltaTime) =>
            // {
            //     Debug.Log("HELLO");
            // });
            // obj.SubscribeOnCollisionEnter((_, collider) =>
            // {
            //     collider.contacts
            // });
        }
    }
}