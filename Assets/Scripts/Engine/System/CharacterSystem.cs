using System;
using System.Collections.Generic;
using Atomic;
using Atomic.Elements;
using Atomic.Objects;
using GameEngine;
using UnityEngine;

namespace Sample
{
    public sealed class CharacterSystem : MonoBehaviour
    {
        [SerializeField]
        private AtomicObject character;

        [ValueId]
        [SerializeField]
        private int attackActionKey;

        private MoveController moveController;
        private JumpController jumpController;


        private Dictionary<int, object> characters;

        private void FixedUpdate()
        {
            int a = 555; //HashCode 555

            const ushort fff = 5;
            int code = fff.GetHashCode();

            int hashCode = "Vasya".GetHashCode();

            GameObject gameObject = this.character.GetValue<GameObject>(CommonAPI.GameObject);


            //
            // object character = this.characters["Vasya"]; //O(1) 
            // //"Vasya" -> hash
            // //hash % bucketCount -> object[]
        }

        private void Awake()
        {
            // this.character.GetAnimator().
            //
            // // GameObject value = this.character.GetValue<GameObject>(CommonAPI.GameObject);
            // // GameObject go = this.character.GetGameObject();
            //
            //
            // MoveComponent moveComponent = this.character.GetMoveComponent();
            //
            //
            // this.character.AddMoveComponent(new MoveComponent());
            //
            // Transform[] patrolPoints = this.character.GetPatrolPoints();
        }


        // private void Start()
        // {
        //     this.moveController = new MoveController(new AtomicSetter<float>(value =>
        //     {
        //         foreach (AtomicObject character in this.characters)
        //         {
        //             IAtomicVariable<float> currentDirection = character.GetMoveComponent()?.CurrentDirection;
        //             
        //             if (currentDirection != null)
        //             {
        //                 currentDirection.Value = value;
        //             }
        //         }
        //     }));
        //
        //     this.jumpController = new JumpController(new AtomicAction(() =>
        //     {
        //         foreach (AtomicObject character in this.characters)
        //         {
        //             character.GetJumpComponent()?.JumpAction?.Invoke();
        //         }
        //     }));
        // }

        private void Update()
        {
            this.moveController.Update();
            this.jumpController.Update();
        }
    }
}