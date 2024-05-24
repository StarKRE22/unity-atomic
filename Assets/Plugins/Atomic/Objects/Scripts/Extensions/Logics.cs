using System;
using UnityEngine;

namespace Atomic.Objects
{
    [Serializable]
    public sealed class EnableLogic : IEnable
    {
        private Action<IObject> action;

        public EnableLogic()
        {
        }

        public EnableLogic(Action<IObject> action)
        {
            this.action = action;
        }

        public void Compose(Action<IObject> action)
        {
            this.action = action;
        }

        public void Enable(IObject obj)
        {
            this.action?.Invoke(obj);
        }
    }

    [Serializable]
    public sealed class DisableLogic : IDisable
    {
        private Action<IObject> action;

        public DisableLogic()
        {
        }

        public DisableLogic(Action<IObject> action)
        {
            this.action = action;
        }

        public void Compose(Action<IObject> action)
        {
            this.action = action;
        }

        public void Disable(IObject obj)
        {
            this.action?.Invoke(obj);
        }
    }

    [Serializable]
    public sealed class UpdateLogic : IUpdate
    {
        private Action<IObject, float> action;

        public UpdateLogic()
        {
        }

        public UpdateLogic(Action<IObject, float> action)
        {
            this.action = action;
        }

        public void Compose(Action<IObject, float> action)
        {
            this.action = action;
        }

        public void OnUpdate(IObject obj, float deltaTime)
        {
            this.action?.Invoke(obj, deltaTime);
        }
    }

    [Serializable]
    public sealed class FixedUpdateLogic : IFixedUpdate
    {
        private Action<IObject, float> action;

        public FixedUpdateLogic()
        {
        }

        public FixedUpdateLogic(Action<IObject, float> action)
        {
            this.action = action;
        }

        public void Compose(Action<IObject, float> action)
        {
            this.action = action;
        }

        public void OnFixedUpdate(IObject obj, float deltaTime)
        {
            this.action?.Invoke(obj, deltaTime);
        }
    }

    [Serializable]
    public sealed class LateUpdateLogic : ILateUpdate
    {
        private Action<IObject, float> action;

        public LateUpdateLogic()
        {
        }

        public LateUpdateLogic(Action<IObject, float> action)
        {
            this.action = action;
        }

        public void Compose(Action<IObject, float> action)
        {
            this.action = action;
        }

        public void OnLateUpdate(IObject obj, float deltaTime)
        {
            this.action?.Invoke(obj, deltaTime);
        }
    }

#if UNITY_EDITOR
    [Serializable]
    public sealed class DrawGizmosLogic : IDrawGizmos
    {
        private Action<IObject> action;

        public DrawGizmosLogic()
        {
        }

        public DrawGizmosLogic(Action<IObject> action)
        {
            this.action = action;
        }

        public void Compose(Action<IObject> action)
        {
            this.action = action;
        }

        public void OnGizmosDraw(IObject obj)
        {
            this.action?.Invoke(obj);
        }
    }
#endif
    
    [Serializable]
    public sealed class TriggerEnterLogic : ITriggerEnter
    {
        private Action<IObject, Collider> action;

        public TriggerEnterLogic()
        {
        }

        public TriggerEnterLogic(Action<IObject, Collider> action)
        {
            this.action = action;
        }

        public void Compose(Action<IObject, Collider> action)
        {
            this.action = action;
        }

        public void TriggerEnter(IObject obj, Collider collider)
        {
            this.action?.Invoke(obj, collider);
        }
    }
    
    [Serializable]
    public sealed class TriggerExitLogic : ITriggerExit
    {
        private Action<IObject, Collider> action;

        public TriggerExitLogic()
        {
        }

        public TriggerExitLogic(Action<IObject, Collider> action)
        {
            this.action = action;
        }

        public void Compose(Action<IObject, Collider> action)
        {
            this.action = action;
        }

        public void TriggerExit(IObject obj, Collider collider)
        {
            this.action?.Invoke(obj, collider);
        }
    }
    
    [Serializable]
    public sealed class CollisionLogic : ICollisionEnter
    {
        private Action<IObject, Collision> action;

        public CollisionLogic()
        {
        }

        public CollisionLogic(Action<IObject, Collision> action)
        {
            this.action = action;
        }

        public void Compose(Action<IObject, Collision> action)
        {
            this.action = action;
        }

        public void CollisionEnter(IObject obj, Collision collider)
        {
            this.action?.Invoke(obj, collider);
        }
    }
    
    [Serializable]
    public sealed class CollisionExitLogic : ICollisionExit
    {
        private Action<IObject, Collision> action;

        public CollisionExitLogic()
        {
        }

        public CollisionExitLogic(Action<IObject, Collision> action)
        {
            this.action = action;
        }

        public void Compose(Action<IObject, Collision> action)
        {
            this.action = action;
        }
        
        public void CollisionExit(IObject obj, Collision collider)
        {
            this.action?.Invoke(obj, collider);
        }
    }
    
    [Serializable]
    public sealed class TriggerEnter2DLogic : ITriggerEnter2D
    {
        private Action<IObject, Collider2D> action;

        public TriggerEnter2DLogic()
        {
        }

        public TriggerEnter2DLogic(Action<IObject, Collider2D> action)
        {
            this.action = action;
        }

        public void Compose(Action<IObject, Collider2D> action)
        {
            this.action = action;
        }

        public void TriggerEnter2D(IObject obj, Collider2D collider)
        {
            this.action?.Invoke(obj, collider);
        }
    }
    
    [Serializable]
    public sealed class TriggerExit2DLogic : ITriggerExit2D
    {
        private Action<IObject, Collider2D> action;

        public TriggerExit2DLogic()
        {
        }

        public TriggerExit2DLogic(Action<IObject, Collider2D> action)
        {
            this.action = action;
        }

        public void Compose(Action<IObject, Collider2D> action)
        {
            this.action = action;
        }

        public void TriggerExit2D(IObject obj, Collider2D collider)
        {
            this.action?.Invoke(obj, collider);
        }
    }
    
    [Serializable]
    public sealed class CollisionEnter2DLogic : ICollisionEnter2D
    {
        private Action<IObject, Collision2D> action;

        public CollisionEnter2DLogic()
        {
        }

        public CollisionEnter2DLogic(Action<IObject, Collision2D> action)
        {
            this.action = action;
        }

        public void Compose(Action<IObject, Collision2D> action)
        {
            this.action = action;
        }

        public void CollisionEnter2D(IObject obj, Collision2D collider)
        {
            this.action?.Invoke(obj, collider);
        }
    }
    
    [Serializable]
    public sealed class CollisionExit2DLogic : ICollisionExit2D
    {
        private Action<IObject, Collision2D> action;

        public CollisionExit2DLogic()
        {
        }

        public CollisionExit2DLogic(Action<IObject, Collision2D> action)
        {
            this.action = action;
        }

        public void Compose(Action<IObject, Collision2D> action)
        {
            this.action = action;
        }
        
        public void CollisionExit2D(IObject obj, Collision2D collider)
        {
            this.action?.Invoke(obj, collider);
        }
    }
    
    public abstract class ScriptableLogic : ScriptableObject, ILogic
    {
    }
}