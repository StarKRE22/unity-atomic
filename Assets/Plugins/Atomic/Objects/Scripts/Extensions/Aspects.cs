using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Objects
{
    [Serializable, InlineProperty]
    public sealed class TagAspect : IAtomicAspect
    {
        [TagId]
        [SerializeField]
        private int tag = -1;

        public int Tag => this.tag;

        public void Compose(IAtomicObject obj)
        {
            obj.AddTag(this.tag);
        }

        public void Dispose(IAtomicObject obj)
        {
            obj.DelTag(this.tag);
        }
    }

    [Serializable, InlineProperty]
    public class ValueAspect<T> : IAtomicAspect
    {
        [ValueId]
        [HorizontalGroup]
        [SerializeField]
        private int id = -1;

        [HideLabel]
        [HorizontalGroup]
        [SerializeField]
        protected T value;

        public T Value => this.value;

        public ValueAspect()
        {
        }

        public ValueAspect(T value)
        {
            this.value = value;
        }

        public virtual void Compose(IAtomicObject obj)
        {
            obj.AddValue(this.id, this.value);
        }

        public virtual void Dispose(IAtomicObject obj)
        {
            obj.DelValue(this.id);
        }
    }


    [Serializable, InlineProperty]
    public class LogicAspect<T> : IAtomicAspect where T : IAtomicLogic
    {
        [SerializeField]
        protected T value;

        public T Value => this.value;

        public LogicAspect()
        {
        }

        public LogicAspect(T value)
        {
            this.value = value;
        }

        public virtual void Compose(IAtomicObject obj)
        {
            obj.AddLogic(this.value);
        }

        public void Dispose(IAtomicObject obj)
        {
            obj.DelLogic(this.value);
        }
    }

    [Serializable]
    public class ElementAspect<T> : ValueAspect<T> where T : IAtomicLogic
    {
        public override void Compose(IAtomicObject obj)
        {
            base.Compose(obj);
            obj.AddLogic(this.value);
        }

        public override void Dispose(IAtomicObject obj)
        {
            base.Dispose(obj);
            obj.DelLogic(this.value);
        }
    }


    [Serializable]
    public sealed class GameObjectAspect : ValueAspect<GameObject>
    {
    }

    [Serializable]
    public sealed class TransformAspect : ValueAspect<Transform>
    {
    }

    [Serializable]
    public sealed class TransformArrayAspect : ValueAspect<Transform[]>
    {
    }

    [Serializable]
    public sealed class ComponentAspect : ValueAspect<Component>
    {
    }
}