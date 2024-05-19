using System;
using UnityEngine;

namespace Atomic.Objects
{
    [Serializable]
    public sealed class AtomicEnable : IAtomicObject.IEnable
    {
        private Action<IAtomicObject> action;

        public AtomicEnable()
        {
        }

        public AtomicEnable(Action<IAtomicObject> action)
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
    public sealed class AtomicDisable : IAtomicObject.IDisable
    {
        private Action<IAtomicObject> action;

        public AtomicDisable()
        {
        }

        public AtomicDisable(Action<IAtomicObject> action)
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
    public sealed class AtomicUpdate : IAtomicObject.IUpdate
    {
        private Action<IAtomicObject, float> action;

        public AtomicUpdate()
        {
        }

        public AtomicUpdate(Action<IAtomicObject, float> action)
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
    public sealed class AtomicFixedUpdate : IAtomicObject.IFixedUpdate
    {
        private Action<IAtomicObject, float> action;

        public AtomicFixedUpdate()
        {
        }

        public AtomicFixedUpdate(Action<IAtomicObject, float> action)
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
    public sealed class AtomicLateUpdate : IAtomicObject.ILateUpdate
    {
        private Action<IAtomicObject, float> action;

        public AtomicLateUpdate()
        {
        }

        public AtomicLateUpdate(Action<IAtomicObject, float> action)
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
    public sealed class AtomicDrawGizmos : IAtomicObject.IDrawGizmos
    {
        private Action<IAtomicObject> action;

        public AtomicDrawGizmos()
        {
        }

        public AtomicDrawGizmos(Action<IAtomicObject> action)
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
    public sealed class AtomicTriggerEnter : IAtomicObject.ITriggerEnter
    {
        private Action<IAtomicObject, Collider> action;

        public AtomicTriggerEnter()
        {
        }

        public AtomicTriggerEnter(Action<IAtomicObject, Collider> action)
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
    public sealed class AtomicTriggerExit : IAtomicObject.ITriggerExit
    {
        private Action<IAtomicObject, Collider> action;

        public AtomicTriggerExit()
        {
        }

        public AtomicTriggerExit(Action<IAtomicObject, Collider> action)
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
    public sealed class AtomicCollisionEnter : IAtomicObject.ICollisionEnter
    {
        private Action<IAtomicObject, Collision> action;

        public AtomicCollisionEnter()
        {
        }

        public AtomicCollisionEnter(Action<IAtomicObject, Collision> action)
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
    public sealed class AtomicCollisionExit : IAtomicObject.ICollisionExit
    {
        private Action<IAtomicObject, Collision> action;

        public AtomicCollisionExit()
        {
        }

        public AtomicCollisionExit(Action<IAtomicObject, Collision> action)
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
    public sealed class AtomicTriggerEnter2D : IAtomicObject.ITriggerEnter2D
    {
        private Action<IAtomicObject, Collider2D> action;

        public AtomicTriggerEnter2D()
        {
        }

        public AtomicTriggerEnter2D(Action<IAtomicObject, Collider2D> action)
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
    public sealed class AtomicTriggerExit2D : IAtomicObject.ITriggerExit2D
    {
        private Action<IAtomicObject, Collider2D> action;

        public AtomicTriggerExit2D()
        {
        }

        public AtomicTriggerExit2D(Action<IAtomicObject, Collider2D> action)
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
    public sealed class AtomicCollisionEnter2D : IAtomicObject.ICollisionEnter2D
    {
        private Action<IAtomicObject, Collision2D> action;

        public AtomicCollisionEnter2D()
        {
        }

        public AtomicCollisionEnter2D(Action<IAtomicObject, Collision2D> action)
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
    public sealed class AtomicCollisionExit2D : IAtomicObject.ICollisionExit2D
    {
        private Action<IAtomicObject, Collision2D> action;

        public AtomicCollisionExit2D()
        {
        }

        public AtomicCollisionExit2D(Action<IAtomicObject, Collision2D> action)
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
}