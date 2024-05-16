using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Atomic.Objects
{
    public interface IAtomicObject
    {
        bool HasTag(int tag);
        bool AddTag(int tag);
        bool DelTag(int tag);
        
        [CanBeNull]
        T GetReference<T>(int id) where T : class;

        [CanBeNull]
        object GetReference(int id);

        bool TryGetReference<T>(int id, out T value) where T : class;
        bool TryGetReference(int id, out object value);

        bool AddReference(int id, object value);
        void SetReference(int id, object value);
        bool DelReference(int id);
        bool DelReference(int id, out object removed);

        bool AddBehaviour(IBehaviour behaviour);
        bool AddBehaviour<T>() where T : IBehaviour, new();
        bool DelBehaviour(IBehaviour behaviour);
        bool DelBehaviour<T>() where T : IBehaviour;

        public interface IComposable
        {
            void Compose(IAtomicObject obj);
        }

        public interface IBehaviour
        {
        }

        public interface IAwake : IBehaviour
        {
            void OnAwake(IAtomicObject obj);
        }

        public interface IDestroy : IBehaviour
        {
            void Destroy(IAtomicObject obj);
        }

        public interface IEnable : IBehaviour
        {
            void Enable(IAtomicObject obj);
        }

        public interface IDisable : IBehaviour
        {
            void Disable(IAtomicObject obj);
        }

        public interface IFixedUpdate : IBehaviour
        {
            void OnFixedUpdate(IAtomicObject obj, float deltaTime);
        }

        public interface ILateUpdate : IBehaviour
        {
            void OnLateUpdate(IAtomicObject obj, float deltaTime);
        }

        public interface IUpdate : IBehaviour
        {
            void OnUpdate(IAtomicObject obj, float deltaTime);
        }

#if UNITY_EDITOR
        //Don't forget wrap #if UNITY_EDITOR!
        public interface IDrawGizmos : IBehaviour
        {
            void OnGizmosDraw(IAtomicObject obj);
        }
#endif


        public interface ITriggerEnter : IBehaviour
        {
            void TriggerEnter(IAtomicObject obj, Collider collider);
        }

        public interface ITriggerExit : IBehaviour
        {
            void TriggerExit(IAtomicObject obj, Collider collider);
        }

        public interface ITriggerEnter2D : IBehaviour
        {
            void TriggerEnter2D(IAtomicObject obj, Collider2D collider);
        }

        public interface ITriggerExit2D : IBehaviour
        {
            void TriggerExit2D(IAtomicObject obj, Collider2D collider);
        }

        public interface ICollisionEnter : IBehaviour
        {
            void CollisionEnter(IAtomicObject obj, Collision collider);
        }

        public interface ICollisionExit : IBehaviour
        {
            void CollisionExit(IAtomicObject obj, Collision collider);
        }

        public interface ICollisionEnter2D : IBehaviour
        {
            void CollisionEnter2D(IAtomicObject obj, Collision2D collider);
        }

        public interface ICollisionExit2D : IBehaviour
        {
            void CollisionExit2D(IAtomicObject obj, Collision2D collider);
        }
        
        public interface IAspect
        {
            void Apply(IAtomicObject target);
            void Discard(IAtomicObject target);
        }

    }
}