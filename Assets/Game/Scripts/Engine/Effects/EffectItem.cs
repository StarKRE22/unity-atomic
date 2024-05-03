using Atomic;
using UnityEngine;

namespace Game.Engine
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
            IAtomicEntity obj = col.GetComponentInParent<IAtomicEntity>();

            if (obj != null && obj.TryGet(MasterAPI.EffectManager, out EffectManager effectManager))
            {
                // Transform objTransform = obj.transform;
                // IEffect effect = Instantiate(this.effectPrefab, objTransform.position, objTransform.rotation, objTransform);
                effectManager.ApplyEffect(this.effect);
                Destroy(this.gameObject);
            }
        }
    }
}