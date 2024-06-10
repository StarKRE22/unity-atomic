using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Atomic.Elements
{
    [AddComponentMenu("Atomic/Elements/Animation Event Receiver")]
    [DisallowMultipleComponent]
    public sealed class AnimationEventListener : MonoBehaviour, ISerializationCallbackReceiver
    {
        public event Action<string> OnMessageReceived;

        [SerializeField]
        [FormerlySerializedAs("events")]
        private Reaction[] reactions;

        private Dictionary<string, List<Action>> eventBus;

        public void Subscribe(string eventName, Action reaction)
        {
            if (string.IsNullOrEmpty(eventName) || reaction == null)
            {
                return;
            }
            
            if (!this.eventBus.TryGetValue(eventName, out List<Action> reactions))
            {
                reactions = new List<Action>();
                this.eventBus.Add(eventName, reactions);
            }
            
            reactions.Add(reaction);
        }

        public void Unsubscribe(string eventName, Action reaction)
        {
            if (string.IsNullOrEmpty(eventName) || reaction == null)
            {
                return;
            }
            
            if (this.eventBus.TryGetValue(eventName, out List<Action> reactions))
            {
                reactions.Remove(reaction);
            }
        }

        [UsedImplicitly]
        internal void ReceiveEvent(string message)
        {
            if (!this.eventBus.TryGetValue(message, out List<Action> reactions))
            {
                return;
            }

            for (int i = 0, count = reactions.Count; i < count; i++)
            {
                Action reaction = reactions[i];
                reaction.Invoke();
            }

            this.OnMessageReceived?.Invoke(message);
            
            // for (int i = 0, count = this.reactions.Length; i < count; i++)
            // {
            //     Reaction @event = this.reactions[i];
            //     if (@event.message == message)
            //     {
            //         @event.action.Invoke();
            //     }
            // }
        }


        public void OnAfterDeserialize()
        {
            this.eventBus = new Dictionary<string, List<Action>>();

            for (int i = 0, count = this.reactions.Length; i < count; i++)
            {
                Reaction reaction = this.reactions[i];
                this.Subscribe(reaction.message, reaction.action.Invoke);
            }
        }

        public void OnBeforeSerialize()
        {
        }

        [Serializable]
        private struct Reaction
        {
            [SerializeField]
            internal string message;

            [SerializeField]
            internal UnityEvent action;
        }
    }
}