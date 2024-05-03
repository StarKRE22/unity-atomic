using AIModule;
using Atomic;
using UnityEngine;

namespace Game.Engine
{
    [CreateAssetMenu(
        fileName = "AIAction_CharacterAction",
        menuName = "Engine/AI/New AIAction_CharacterAction"
    )]
    public sealed class AIAction_CharacterAction : AIAction
    {
        [SerializeField, BlackboardKey]
        private ushort character;

        [SerializeField]
        private string actionName;

        public override void Perform(IBlackboard blackboard)
        {
            if (blackboard.TryGetObject(this.character, out IAtomicObject character))
            {
                character.InvokeAction(this.actionName);
            }
        }
    }
}