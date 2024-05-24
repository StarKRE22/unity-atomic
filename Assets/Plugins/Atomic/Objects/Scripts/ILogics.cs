using UnityEngine;

namespace Atomic.Objects
{
    public interface ILogic
    {
    }

    public interface IEnable : ILogic
    {
        void Enable(IObject obj);
    }

    public interface IDisable : ILogic
    {
        void Disable(IObject obj);
    }

    public interface IFixedUpdate : ILogic
    {
        void OnFixedUpdate(IObject obj, float deltaTime);
    }

    public interface ILateUpdate : ILogic
    {
        void OnLateUpdate(IObject obj, float deltaTime);
    }

    public interface IUpdate : ILogic
    {
        void OnUpdate(IObject obj, float deltaTime);
    }

#if UNITY_EDITOR
    //Don't forget wrap #if UNITY_EDITOR!
    public interface IDrawGizmos : ILogic
    {
        void OnGizmosDraw(IObject obj);
    }
#endif

    public interface ITriggerEnter : ILogic
    {
        void TriggerEnter(IObject obj, Collider collider);
    }

    public interface ITriggerExit : ILogic
    {
        void TriggerExit(IObject obj, Collider collider);
    }

    public interface ICollisionEnter : ILogic
    {
        void CollisionEnter(IObject obj, Collision collider);
    }

    public interface ICollisionExit : ILogic
    {
        void CollisionExit(IObject obj, Collision collider);
    }

    public interface ITriggerEnter2D : ILogic
    {
        void TriggerEnter2D(IObject obj, Collider2D collider);
    }

    public interface ITriggerExit2D : ILogic
    {
        void TriggerExit2D(IObject obj, Collider2D collider);
    }

    public interface ICollisionEnter2D : ILogic
    {
        void CollisionEnter2D(IObject obj, Collision2D collider);
    }

    public interface ICollisionExit2D : ILogic
    {
        void CollisionExit2D(IObject obj, Collision2D collider);
    }
}