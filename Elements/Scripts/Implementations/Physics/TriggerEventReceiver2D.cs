using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Elements
{
    [AddComponentMenu("Atomic/Elements/Trigger Event Receiver 2D")]
    [DisallowMultipleComponent]
    public sealed class TriggerEventReceiver2D : MonoBehaviour
    {
        public event Action<Collider2D> OnEntered; 
        public event Action<Collider2D> OnExited; 
        public event Action<Collider2D> OnStay;

        [SerializeReference]
        public List<IColliderAction2D> enterActions = new();

        [SerializeReference]
        public List<IColliderAction2D> exitActions = new();
        
        [SerializeReference]
        public List<IColliderAction2D> stayActions = new();

        private void OnTriggerEnter2D(Collider2D collider)
        {
            for (int i = 0, count = this.enterActions.Count; i < count; i++)
            {
                IColliderAction2D action = this.enterActions[i];
                action?.Invoke(collider);
            }
            
            this.OnEntered?.Invoke(collider);
        }

        private void OnTriggerStay2D(Collider2D collider)
        {
            if (this.stayActions != null)
            {
                for (int i = 0, count = this.stayActions.Count; i < count; i++)
                {
                    IColliderAction2D action = this.stayActions[i];
                    action?.Invoke(collider);
                }
            }
            
            this.OnStay?.Invoke(collider);
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (this.exitActions != null)
            {
                for (int i = 0, count = this.exitActions.Count; i < count; i++)
                {
                    IColliderAction2D action = this.exitActions[i];
                    action?.Invoke(collider);
                }
            }
            
            this.OnExited?.Invoke(collider);
        }
    }
}