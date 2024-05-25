using UnityEngine;

namespace Atomic.Objects
{
    [CreateAssetMenu(
        fileName = "ScriptableComposer",
        menuName = "Atomic/Objects/New ScriptableComposer"
    )]
    public sealed class ScriptableComposer : ScriptableObject, IComposer
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
        // public void Dispose(IAtomicObject obj)
        // {
        //     if (this.aspects is {Length: > 0})
        //     {
        //         for (int i = 0, count = this.aspects.Length; i < count; i++)
        //         {
        //             IAspect aspect = this.aspects[i];
        //             if (aspect != null)
        //             {
        //                 aspect.Dispose(obj);
        //             }
        //         }
        //     }
        // }
    }
}