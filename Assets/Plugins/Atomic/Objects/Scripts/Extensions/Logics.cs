using System;
using UnityEngine;

namespace Atomic.Objects
{
    [Serializable]
    public sealed class EnableLogic : IEnable
    {
        private Action<IAtomicObject> action;

        public EnableLogic()
        {
        }

        public EnableLogic(Action<IAtomicObject> action)
        {
            this.action = action;
        }

        public void Compose(Action<IAtomicObject> action)
        {
            this.action = action;
        }

        public void Enable(IAtomicObject obj)
        {
            this.action?.Invoke(obj);
        }
    }

    [Serializable]
    public sealed class DisableLogic : IDisable
    {
        private Action<IAtomicObject> action;

        public DisableLogic()
        {
        }

        public DisableLogic(Action<IAtomicObject> action)
        {
            this.action = action;
        }

        public void Compose(Action<IAtomicObject> action)
        {
            this.action = action;
        }

        public void Disable(IAtomicObject obj)
        {
            this.action?.Invoke(obj);
        }
    }

    [Serializable]
    public sealed class UpdateLogic : IUpdate
    {
        private Action<IAtomicObject, float> action;

        public UpdateLogic()
        {
        }

        public UpdateLogic(Action<IAtomicObject, float> action)
        {
            this.action = action;
        }

        public void Compose(Action<IAtomicObject, float> action)
        {
            this.action = action;
        }

        public void OnUpdate(IAtomicObject obj, float deltaTime)
        {
            this.action?.Invoke(obj, deltaTime);
        }
    }

    [Serializable]
    public sealed class FixedUpdateLogic : IFixedUpdate
    {
        private Action<IAtomicObject, float> action;

        public FixedUpdateLogic()
        {
        }

        public FixedUpdateLogic(Action<IAtomicObject, float> action)
        {
            this.action = action;
        }

        public void Compose(Action<IAtomicObject, float> action)
        {
            this.action = action;
        }

        public void OnFixedUpdate(IAtomicObject obj, float deltaTime)
        {
            this.action?.Invoke(obj, deltaTime);
        }
    }

    [Serializable]
    public sealed class LateUpdateLogic : ILateUpdate
    {
        private Action<IAtomicObject, float> action;

        public LateUpdateLogic()
        {
        }

        public LateUpdateLogic(Action<IAtomicObject, float> action)
        {
            this.action = action;
        }

        public void Compose(Action<IAtomicObject, float> action)
        {
            this.action = action;
        }

        public void OnLateUpdate(IAtomicObject obj, float deltaTime)
        {
            this.action?.Invoke(obj, deltaTime);
        }
    }

#if UNITY_EDITOR
    [Serializable]
    public sealed class DrawGizmosLogic : IDrawGizmos
    {
        private Action<IAtomicObject> action;

        public DrawGizmosLogic()
        {
        }

        public DrawGizmosLogic(Action<IAtomicObject> action)
        {
            this.action = action;
        }

        public void Compose(Action<IAtomicObject> action)
        {
            this.action = action;
        }

        public void OnGizmosDraw(IAtomicObject obj)
        {
            this.action?.Invoke(obj);
        }
    }
#endif
    
    [Serializable]
    public sealed class TriggerEnterLogic : ITriggerEnter
    {
        private Action<IAtomicObject, Collider> action;

        public TriggerEnterLogic()
        {
        }

        public TriggerEnterLogic(Action<IAtomicObject, Collider> action)
        {
            this.action = action;
        }

        public void Compose(Action<IAtomicObject, Collider> action)
        {
            this.action = action;
        }

        public void TriggerEnter(IAtomicObject obj, Collider collider)
        {
            this.action?.Invoke(obj, collider);
        }
    }
    
    [Serializable]
    public sealed class TriggerExitLogic : ITriggerExit
    {
        private Action<IAtomicObject, Collider> action;

        public TriggerExitLogic()
        {
        }

        public TriggerExitLogic(Action<IAtomicObject, Collider> action)
        {
            this.action = action;
        }

        public void Compose(Action<IAtomicObject, Collider> action)
        {
            this.action = action;
        }

        public void TriggerExit(IAtomicObject obj, Collider collider)
        {
            this.action?.Invoke(obj, collider);
        }
    }
    
    [Serializable]
    public sealed class CollisionLogic : ICollisionEnter
    {
        private Action<IAtomicObject, Collision> action;

        public CollisionLogic()
        {
        }

        public CollisionLogic(Action<IAtomicObject, Collision> action)
        {
            this.action = action;
        }

        public void Compose(Action<IAtomicObject, Collision> action)
        {
            this.action = action;
        }

        public void CollisionEnter(IAtomicObject obj, Collision collider)
        {
            this.action?.Invoke(obj, collider);
        }
    }
    
    [Serializable]
    public sealed class CollisionExitLogic : ICollisionExit
    {
        private Action<IAtomicObject, Collision> action;

        public CollisionExitLogic()
        {
        }

        public CollisionExitLogic(Action<IAtomicObject, Collision> action)
        {
            this.action = action;
        }

        public void Compose(Action<IAtomicObject, Collision> action)
        {
            this.action = action;
        }
        
        public void CollisionExit(IAtomicObject obj, Collision collider)
        {
            this.action?.Invoke(obj, collider);
        }
    }
    
    [Serializable]
    public sealed class TriggerEnter2DLogic : ITriggerEnter2D
    {
        private Action<IAtomicObject, Collider2D> action;

        public TriggerEnter2DLogic()
        {
        }

        public TriggerEnter2DLogic(Action<IAtomicObject, Collider2D> action)
        {
            this.action = action;
        }

        public void Compose(Action<IAtomicObject, Collider2D> action)
        {
            this.action = action;
        }

        public void TriggerEnter2D(IAtomicObject obj, Collider2D collider)
        {
            this.action?.Invoke(obj, collider);
        }
    }
    
    [Serializable]
    public sealed class TriggerExit2DLogic : ITriggerExit2D
    {
        private Action<IAtomicObject, Collider2D> action;

        public TriggerExit2DLogic()
        {
        }

        public TriggerExit2DLogic(Action<IAtomicObject, Collider2D> action)
        {
            this.action = action;
        }

        public void Compose(Action<IAtomicObject, Collider2D> action)
        {
            this.action = action;
        }

        public void TriggerExit2D(IAtomicObject obj, Collider2D collider)
        {
            this.action?.Invoke(obj, collider);
        }
    }
    
    [Serializable]
    public sealed class CollisionEnter2DLogic : ICollisionEnter2D
    {
        private Action<IAtomicObject, Collision2D> action;

        public CollisionEnter2DLogic()
        {
        }

        public CollisionEnter2DLogic(Action<IAtomicObject, Collision2D> action)
        {
            this.action = action;
        }

        public void Compose(Action<IAtomicObject, Collision2D> action)
        {
            this.action = action;
        }

        public void CollisionEnter2D(IAtomicObject obj, Collision2D collider)
        {
            this.action?.Invoke(obj, collider);
        }
    }
    
    [Serializable]
    public sealed class CollisionExit2DLogic : ICollisionExit2D
    {
        private Action<IAtomicObject, Collision2D> action;

        public CollisionExit2DLogic()
        {
        }

        public CollisionExit2DLogic(Action<IAtomicObject, Collision2D> action)
        {
            this.action = action;
        }

        public void Compose(Action<IAtomicObject, Collision2D> action)
        {
            this.action = action;
        }
        
        public void CollisionExit2D(IAtomicObject obj, Collision2D collider)
        {
            this.action?.Invoke(obj, collider);
        }
    }
    
    public abstract class ScriptableLogic : ScriptableObject, ILogic
    {
    }
}