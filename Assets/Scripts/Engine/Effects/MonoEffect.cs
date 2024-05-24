using System;
using Atomic.Objects;
using UnityEngine;

namespace Sample
{
    public sealed class MonoEffect : MonoBehaviour, ICompletableEffect
    {
        [SerializeField]
        private ScriptableEffect[] scriptableEffects;

        [SerializeReference]
        private IEffect[] plainEffects;

        private Action<IEffect> callback;

        private void Awake()
        {
            foreach (var effect in this.plainEffects)
            {
                if (effect is ICompletableEffect completable)
                {
                    completable.SetCallback(this.OnComplete);
                }
            }
        }

        private void OnComplete(IEffect _)
        {
            this.callback.Invoke(this);
        }

        public void Apply(IObject obj)
        {
            foreach (var effect in this.scriptableEffects)
            {
                effect.Apply(obj);
            }

            foreach (var effect in this.plainEffects)
            {
                effect.Apply(obj);
            }
        }

        public void Discard(IObject obj)
        {
            foreach (var effect in this.scriptableEffects)
            {
                effect.Discard(obj);
            }
            
            foreach (var effect in this.plainEffects)
            {
                effect.Discard(obj);
            }
        }

        void ICompletableEffect.SetCallback(Action<IEffect> callback)
        {
            this.callback = callback;
        }
    }
}