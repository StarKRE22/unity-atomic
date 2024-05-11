using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Sample
{
    public sealed class MoveAspect : ScriptableObject, IAtomicObject.IAspect
    {
        [SerializeField]
        public float baseSpeed;
        
        public void Apply(IAtomicObject target)
        {
            Rigidbody2D rigidbody2D = target.GetRigidbody2D();
            if (rigidbody2D != null)
            {
                target.AddElement(CommonAPI.MoveComponent, new MoveComponent(rigidbody2D, this.baseSpeed));
            }
        }

        public void Discard(IAtomicObject target)
        {
            target.DelElement(CommonAPI.MoveComponent);
        }
    }
}