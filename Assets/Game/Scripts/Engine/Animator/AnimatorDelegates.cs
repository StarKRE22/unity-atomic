using UnityEngine;

namespace Game.Engine
{
    public delegate void AnimatorEventDelegate(string animEvent);
    public delegate void AnimatorStateDelegate(string stateId, AnimatorStateInfo stateInfo, int layerIndex);
}