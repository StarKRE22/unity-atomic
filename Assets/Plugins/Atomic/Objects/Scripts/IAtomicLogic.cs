using UnityEngine;

namespace Atomic.Objects
{
    public interface ILogic
    {
    }

    public interface IAwake : ILogic
    {
        void OnAwake(IAtomicObject obj);
    }
    
    public interface IEnable : ILogic
    {
        void Enable(IAtomicObject obj);
    }

    public interface IDisable : ILogic
    {
        void Disable(IAtomicObject obj);
    }
    
    public interface IFixedUpdate : ILogic
    {
        void OnFixedUpdate(IAtomicObject obj, float deltaTime);
    }

    public interface ILateUpdate : ILogic
    {
        void OnLateUpdate(IAtomicObject obj, float deltaTime);
    }

    public interface IUpdate : ILogic
    {
        void OnUpdate(IAtomicObject obj, float deltaTime);
    }

    public interface IDrawGizmos : ILogic
    {
        void OnGizmosDraw(IAtomicObject obj);
    }

    public interface ITriggerEnter : ILogic
    {
        void TriggerEnter(IAtomicObject obj, Collider trigger);
    }

    public interface ITriggerExit : ILogic
    {
        void TriggerExit(IAtomicObject obj, Collider trigger);
    }

    public interface ICollisionEnter : ILogic
    {
        void CollisionEnter(IAtomicObject obj, Collision collision);
    }

    public interface ICollisionExit : ILogic
    {
        void CollisionExit(IAtomicObject obj, Collision collision);
    }

    public interface ITriggerEnter2D : ILogic
    {
        void TriggerEnter2D(IAtomicObject obj, Collider2D trigger);
    }

    public interface ITriggerExit2D : ILogic
    {
        void TriggerExit2D(IAtomicObject obj, Collider2D trigger);
    }

    public interface ICollisionEnter2D : ILogic
    {
        void CollisionEnter2D(IAtomicObject obj, Collision2D collision);
    }

    public interface ICollisionExit2D : ILogic
    {
        void CollisionExit2D(IAtomicObject obj, Collision2D collision);
    }
}