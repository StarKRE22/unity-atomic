using Atomic;
using UnityEngine;

namespace Game.Engine
{
    public sealed class CharacterSystem : MonoBehaviour
    {
        [SerializeField]
        private AtomicObject character;

        private MoveController moveController;
        private ActionController attackController;
        private ActionController meleeWeaponController;
        private ActionController rangeWeaponController;

        private void Start()
        {
            this.moveController = new MoveController(
                this.character.GetSetter<float>(MovementAPI.MoveDirection)
            );
            this.attackController = new ActionController(
                KeyCode.Space, this.character.GetAction(AttackAPI.AttackRequest)
            );
            this.meleeWeaponController = new ActionController(
                KeyCode.Q, this.character.GetAction(AttackAPI.SwitchToMeleeWeapon)
            );
            this.rangeWeaponController = new ActionController(
                KeyCode.W, this.character.GetAction(AttackAPI.SwitchToRangeWeapon)
            );
        }

        private void Update()
        {
            this.moveController.Update();
            this.attackController.Update();
            this.meleeWeaponController.Update();
            this.rangeWeaponController.Update();
        }
    }
}