using System;
using Atomic.Objects;
using UnityEngine;

namespace Sample
{
    [Serializable]
    public sealed class EffectComposite : ICompletableEffect, ISerializationCallbackReceiver
    {
        [SerializeField]
        private ScriptableEffect[] scriptableEffects;

        [SerializeReference]
        private IEffect[] plainEffects;

        private Action<IEffect> callback;

        private void OnComplete(IEffect _)
        {
            this.callback.Invoke(this);
        }

        public void Apply(IAtomicObject obj)
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

        public void Discard(IAtomicObject obj)
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

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            foreach (var effect in this.plainEffects)
            {
                if (effect is ICompletableEffect completable)
                {
                    completable.SetCallback(this.OnComplete);
                }
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }
    }
}