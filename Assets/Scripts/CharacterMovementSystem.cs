using System;
using Atomic;
using UnityEngine;

namespace DefaultNamespace
{
    public sealed class CharacterMovementSystem : MonoBehaviour
    {
        [SerializeField]
        private Vector3 inputDirection = Vector3.forward;
        
        private AtomicEntityFilter _filter;
        
        private AtomicEntityValues<IAtomicAction<Vector3>> _moveActions;
        private AtomicEntityTypes _characters;

        private void Awake()
        {
            _moveActions = AtomicEntities.GetValues<IAtomicAction<Vector3>>(ObjectAPI.MoveAction);
            _characters = AtomicEntities.GetTypes(ObjectTypes.Character);
            
            _filter = AtomicEntities.GetFilter()
                .Types(ObjectTypes.Character, ObjectTypes.Moveable)
                .Values(ObjectAPI.MoveAction)
                .Condition(entity => _players.Get(entity).IsPlayer == this.isPlayer)
                .Resolve();
        }
        

        private void FixedUpdate()
        {
            foreach (int entity in _filter)
            {
                _moveActions[entity].Invoke(this.inputDirection);
            }
        }
    }
}