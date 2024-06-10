using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Elements
{
    [AddComponentMenu("Atomic/Elements/Collision Event Receiver 2D")]
    [DisallowMultipleComponent]
    public sealed class CollisionEventReceiver2D : MonoBehaviour
    {
        public event Action<Collision2D> OnEntered; 
        public event Action<Collision2D> OnExited; 
        public event Action<Collision2D> OnStay;

        [SerializeReference]
        public List<ICollisionAction2D> enterActions = new();

        [SerializeReference]
        public List<ICollisionAction2D> exitActions = new();
        
        [SerializeReference]
        public List<ICollisionAction2D> stayActions = new();

        private void OnCollisionEnter2D(Collision2D collision)
        {
            for (int i = 0, count = this.enterActions.Count; i < count; i++)
            {
                ICollisionAction2D action = this.enterActions[i];
                action?.Invoke(collision);
            }
            
            this.OnEntered?.Invoke(collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (this.stayActions != null)
            {
                for (int i = 0, count = this.stayActions.Count; i < count; i++)
                {
                    ICollisionAction2D action = this.stayActions[i];
                    action?.Invoke(collision);
                }
            }
            
            this.OnStay?.Invoke(collision);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (this.exitActions != null)
            {
                for (int i = 0, count = this.exitActions.Count; i < count; i++)
                {
                    ICollisionAction2D action = this.exitActions[i];
                    action?.Invoke(collision);
                }
            }
            
            this.OnExited?.Invoke(collision);
        }
    }
}