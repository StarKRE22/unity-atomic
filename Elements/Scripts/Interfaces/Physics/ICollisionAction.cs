using UnityEngine;

namespace Atomic.Elements
{
    public interface ICollisionAction : IAtomicAction<Collision>
    {
    }
    
    public interface ICollisionAction2D : IAtomicAction<Collision2D>
    {
    }
}