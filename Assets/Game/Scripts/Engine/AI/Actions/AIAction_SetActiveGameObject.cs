using AIModule;
using UnityEngine;

namespace Game.Engine
{
    [CreateAssetMenu(
        fileName = "AIAction_SetActiveGameObject",
        menuName = "Engine/AI/New AIAction_SetActiveGameObject"
    )]
    public sealed class AIAction_SetActiveGameObject : AIAction
    {
        [BlackboardKey, SerializeField]
        private ushort gameObjectKey;

        [SerializeField]
        private bool active = true;

        public override void Perform(IBlackboard blackboard)
        {
            if (blackboard.TryGetObject(this.gameObjectKey, out GameObject obj))
            {
                obj.SetActive(this.active);
            }
        }
    }
}