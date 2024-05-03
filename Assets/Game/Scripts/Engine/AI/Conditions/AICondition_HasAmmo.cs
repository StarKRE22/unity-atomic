using AIModule;
using Atomic;
using UnityEngine;

namespace Game.Engine
{
    [CreateAssetMenu(
        fileName = "AICondition_HasAmmo",
        menuName = "Engine/AI/New AICondition_HasAmmo"
    )]
    public sealed class AICondition_HasAmmo : AICondition
    {
        [SerializeField, BlackboardKey]
        private ushort character;

        public override bool Check(IBlackboard blackboard)
        {
            if (!blackboard.TryGetObject(this.character, out IAtomicObject character) ||
                !character.TryGet(AttackAPI.WeaponCharges, out IAtomicValue<int> charges))
            {
                return false;
            }

            return charges.Value > 0;
        }
    }
}