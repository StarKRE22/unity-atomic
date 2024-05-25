using UnityEngine;

namespace Atomic.Objects
{
    [AddComponentMenu("Atomic/Mono Composer")]
    [DisallowMultipleComponent]
    public sealed class MonoComposer : MonoBehaviour, IComposer
    {
        [SerializeReference]
        private IComposer[] composers;

        public void Compose(IAtomicObject obj)
        {
            if (this.composers is {Length: > 0})
            {
                for (int i = 0, count = this.composers.Length; i < count; i++)
                {
                    IComposer aspect = this.composers[i];
                    if (aspect != null)
                    {
                        aspect.Compose(obj);
                    }
                }
            }
        }

        //TODO: ???
        // public void Discard(IAtomicObject obj)
        // {
        //     if (this.aspects is {Length: > 0})
        //     {
        //         for (int i = 0, count = this.aspects.Length; i < count; i++)
        //         {
        //             IAspect aspect = this.aspects[i];
        //             if (aspect != null)
        //             {
        //                 aspect.Discard(obj);
        //             }
        //         }
        //     }
        // }
    }
}