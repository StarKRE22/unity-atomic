using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Engine
{
    [RequireComponent(typeof(Animator))]
    [DisallowMultipleComponent]
    public class AnimatorDispatcher : MonoBehaviour
    {
        public event AnimatorStateDelegate OnStateEntered;
        public event AnimatorStateDelegate OnStateExited;
        
        private readonly Dictionary<string, Action> eventListeners = new();
        
        public void AddListener(string animEvent, Action action)
        {
            if (eventListeners.ContainsKey(animEvent))
                eventListeners[animEvent] += action;
            else
                eventListeners[animEvent] = action;
        }
        
        public void RemoveListener(string animEvent, Action action)
        {
            if (!eventListeners.ContainsKey(animEvent))
                return;
            
            eventListeners[animEvent] -= action;

            if (eventListeners[animEvent] == null || eventListeners[animEvent].GetInvocationList().Length == 0)
                eventListeners.Remove(animEvent);
        }
        
        ///Called by animation events
        [UsedImplicitly]
        internal void ReceiveEvent(string animEvent)
        {
            if (eventListeners.TryGetValue(animEvent, out var listener))
                listener?.Invoke();
            else
                Debug.LogWarning($"{animEvent} event is not handled");
        }
        
        
        internal void ReceiveStateEnter(string stateId, AnimatorStateInfo stateInfo, int layerIndex)
        {
            this.OnStateEntered?.Invoke(stateId, stateInfo, layerIndex);
        }

        internal void ReceiveStateExit(string stateId, AnimatorStateInfo stateInfo, int layerIndex)
        {
            this.OnStateExited?.Invoke(stateId, stateInfo, layerIndex);
        }
    }
}