using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Elements
{
    [AddComponentMenu("Atomic/Elements/Trigger Event Receiver")]
    [DisallowMultipleComponent]
    public sealed class TriggerEventReceiver : MonoBehaviour
    {
        public event Action<Collider> OnEntered; 
        public event Action<Collider> OnExited; 
        public event Action<Collider> OnStay;

        [SerializeReference]
        public List<IColliderAction> enterActions = new();

        [SerializeReference]
        public List<IColliderAction> exitActions = new();
        
        [SerializeReference]
        public List<IColliderAction> stayActions = new();

        private void OnTriggerEnter(Collider collider)
        {
            for (int i = 0, count = this.enterActions.Count; i < count; i++)
            {
                IColliderAction action = this.enterActions[i];
                action?.Invoke(collider);
            }
            
            this.OnEntered?.Invoke(collider);
        }

        private void OnTriggerStay(Collider collider)
        {
            if (this.stayActions != null)
            {
                for (int i = 0, count = this.stayActions.Count; i < count; i++)
                {
                    IColliderAction action = this.stayActions[i];
                    action?.Invoke(collider);
                }
            }
            
            this.OnStay?.Invoke(collider);
        }

        private void OnTriggerExit(Collider collider)
        {
            if (this.exitActions != null)
            {
                for (int i = 0, count = this.exitActions.Count; i < count; i++)
                {
                    IColliderAction action = this.exitActions[i];
                    action?.Invoke(collider);
                }
            }
            
            this.OnExited?.Invoke(collider);
        }
    }
}