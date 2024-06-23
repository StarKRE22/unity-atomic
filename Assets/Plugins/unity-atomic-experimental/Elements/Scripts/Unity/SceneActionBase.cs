using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Elements
{
    [AddComponentMenu("Atomic/Elements/Scene Action")]
    public abstract class SceneActionBase : MonoBehaviour, IAtomicAction
    {
        public abstract void Invoke();
    }
}