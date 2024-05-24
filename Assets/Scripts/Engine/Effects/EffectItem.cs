using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Sample
{
    public sealed class EffectItem : MonoBehaviour
    {
        // [SerializeField]
        // private ScriptableEffect effect;

        // [SerializeField]
        // private CompositeEffect effectPrefab;

        [SerializeReference]
        private IEffect effect;

        private void OnTriggerEnter2D(Collider2D col)
        {
            var obj = col.GetComponentInParent<IAtomicObject>();

            
            
            if (obj.TryGetValue(CommonAPI.EffectHolder, out EffectHolder effectHolder))
            {
                // Transform objTransform = obj.transform;
                // IEffect effect = Instantiate(this.effectPrefab, objTransform.position, objTransform.rotation, objTransform);
                effectHolder.ApplyEffect(this.effect);
                Destroy(this.gameObject);
            }
        }
    }
}