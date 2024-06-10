using UnityEngine;

namespace Atomic.Elements
{
    public interface IColliderAction : IAtomicAction<Collider>
    {
    }
    
    public interface IColliderAction2D : IAtomicAction<Collider2D>
    {
    }
}