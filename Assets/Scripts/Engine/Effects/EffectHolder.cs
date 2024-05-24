using System;
using System.Collections.Generic;
using Atomic.Objects;
using Sirenix.OdinInspector;

namespace Sample
{
    [Serializable]
    public sealed class EffectHolder
    {
        public event Action<IEffect> OnEffectAdded;
        public event Action<IEffect> OnEffectRemoved; 

        public IReadOnlyList<IEffect> Effects => this.effects;

        [ShowInInspector, ReadOnly]
        private readonly List<IEffect> effects = new();
        
        private IObject owner;

        public EffectHolder()
        {
        }

        public EffectHolder(IObject owner)
        {
            this.owner = owner;
        }

        public void Compose(IObject owner)
        {
            this.owner = owner;
        }
        
        [Button]
        public void ApplyEffect(IEffect effect)
        {
            if (effect is ICompletableEffect completable)
            {
                completable.SetCallback(this.DiscardEffect);
            }
            
            this.effects.Add(effect);
            
             effect.Apply(this.owner);
            this.OnEffectAdded?.Invoke(effect);
        }

        [Button]
        public void DiscardEffect(IEffect effect)
        {
            if (this.effects.Remove(effect))
            {
                effect.Discard(this.owner);
                this.OnEffectRemoved?.Invoke(effect);
            }
        }

        [Button]
        public void DiscardAllEffects()
        {
            foreach (var effect in this.effects)
            {
                effect.Discard(this.owner);
            }
            
            this.effects.Clear();
        }
    }
}