using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Objects
{
    [Serializable, InlineProperty]
    public sealed class TagComposable : IComposable
    {
        [TagId]
        [SerializeField]
        private int tag = -1;

        public int Tag => this.tag;

        public void Compose(IObject obj)
        {
            obj.AddTag(this.tag);
        }
    }

    [Serializable, InlineProperty]
    public class ValueComposable<T> : IComposable
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

        public ValueComposable()
        {
        }

        public ValueComposable(T value)
        {
            this.value = value;
        }

        public virtual void Compose(IObject obj)
        {
            obj.AddValue(this.id, this.value);
        }
    }


    [Serializable, InlineProperty]
    public class LogicComposable<T> : IComposable where T : ILogic
    {
        [SerializeField]
        protected T value;

        public T Value => this.value;

        public LogicComposable()
        {
        }

        public LogicComposable(T value)
        {
            this.value = value;
        }

        public virtual void Compose(IObject obj)
        {
            obj.AddLogic(this.value);
        }
    }

    [Serializable]
    public class ElementComposable<T> : ValueComposable<T> where T : ILogic
    {
        public override void Compose(IObject obj)
        {
            base.Compose(obj);
            obj.AddLogic(this.value);
        }
    }


    [Serializable]
    public sealed class GameObjectComposable : ValueComposable<GameObject>
    {
    }

    [Serializable]
    public sealed class TransformComposable : ValueComposable<Transform>
    {
    }

    [Serializable]
    public sealed class TransformArrayComposable : ValueComposable<Transform[]>
    {
    }

    [Serializable]
    public sealed class ComponentComposable : ValueComposable<Component>
    {
    }

    [Serializable]
    public sealed class ScriptableObjectComposable : ValueComposable<ScriptableObject>
    {
    }
}