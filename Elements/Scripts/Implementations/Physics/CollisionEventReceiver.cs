using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Elements
{
    [AddComponentMenu("Atomic/Elements/Collision Event Receiver")]
    [DisallowMultipleComponent]
    public sealed class CollisionEventReceiver : MonoBehaviour
    {
        public event Action<Collision> OnEntered; 
        public event Action<Collision> OnExited; 
        public event Action<Collision> OnStay;

        [SerializeReference]
        public List<ICollisionAction> enterActions = new();

        [SerializeReference]
        public List<ICollisionAction> exitActions = new();
        
        [SerializeReference]
        public List<ICollisionAction> stayActions = new();

        private void OnCollisionEnter(Collision collision)
        {
            for (int i = 0, count = this.enterActions.Count; i < count; i++)
            {
                ICollisionAction action = this.enterActions[i];
                action?.Invoke(collision);
            }
            
            this.OnEntered?.Invoke(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            if (this.stayActions != null)
            {
                for (int i = 0, count = this.stayActions.Count; i < count; i++)
                {
                    ICollisionAction action = this.stayActions[i];
                    action?.Invoke(collision);
                }
            }
            
            this.OnStay?.Invoke(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            if (this.exitActions != null)
            {
                for (int i = 0, count = this.exitActions.Count; i < count; i++)
                {
                    ICollisionAction action = this.exitActions[i];
                    action?.Invoke(collision);
                }
            }
            
            this.OnExited?.Invoke(collision);
        }
    }
}