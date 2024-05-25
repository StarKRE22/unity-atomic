using UnityEngine;

namespace Atomic.Objects
{
    public interface IAtomicLogic
    {
    }

    public interface IAtomicEnable : IAtomicLogic
    {
        void Enable(IAtomicObject obj);
    }

    public interface IAtomicDisable : IAtomicLogic
    {
        void Disable(IAtomicObject obj);
    }

    public interface IAtomicFixedUpdate : IAtomicLogic
    {
        void OnFixedUpdate(IAtomicObject obj, float deltaTime);
    }

    public interface ILateUpdate : IAtomicLogic
    {
        void OnLateUpdate(IAtomicObject obj, float deltaTime);
    }

    public interface IUpdate : IAtomicLogic
    {
        void OnUpdate(IAtomicObject obj, float deltaTime);
    }

#if UNITY_EDITOR
    //Don't forget wrap #if UNITY_EDITOR!
    public interface IDrawGizmos : IAtomicLogic
    {
        void OnGizmosDraw(IAtomicObject obj);
    }
#endif

    public interface ITriggerEnter : IAtomicLogic
    {
        void TriggerEnter(IAtomicObject obj, Collider collider);
    }

    public interface ITriggerExit : IAtomicLogic
    {
        void TriggerExit(IAtomicObject obj, Collider collider);
    }

    public interface ICollisionEnter : IAtomicLogic
    {
        void CollisionEnter(IAtomicObject obj, Collision collider);
    }

    public interface ICollisionExit : IAtomicLogic
    {
        void CollisionExit(IAtomicObject obj, Collision collider);
    }

    public interface ITriggerEnter2D : IAtomicLogic
    {
        void TriggerEnter2D(IAtomicObject obj, Collider2D collider);
    }

    public interface ITriggerExit2D : IAtomicLogic
    {
        void TriggerExit2D(IAtomicObject obj, Collider2D collider);
    }

    public interface ICollisionEnter2D : IAtomicLogic
    {
        void CollisionEnter2D(IAtomicObject obj, Collision2D collider);
    }

    public interface ICollisionExit2D : IAtomicLogic
    {
        void CollisionExit2D(IAtomicObject obj, Collision2D collider);
    }
}