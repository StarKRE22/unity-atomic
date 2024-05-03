using Atomic;
using UnityEngine;

namespace Game.Content
{
    public abstract class Weapon : MonoBehaviour
    {
        public abstract IAtomicFunction<bool> FireCondition { get; }
    }
}