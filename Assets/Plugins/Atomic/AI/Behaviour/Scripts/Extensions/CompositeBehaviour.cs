using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
// ReSharper disable NotAccessedField.Local

namespace Modules.AI
{
    [Serializable]
    public sealed class CompositeBehaviour :
        IStartBehaviour,
        IStopBehaviour,
        IUpdateBehaviour,
        ISerializationCallbackReceiver
    {
        #if UNITY_EDITOR

        [GUIColor(1f, 0.92156863f, 0.015686275f)]
        [SerializeField, HideLabel]
        private string name;
        
        #endif
        
        [SerializeReference]
        private List<IBehaviour> behaviours = default;

        private List<IUpdateBehaviour> updateBehaviours = new();

        public void OnStart(IBlackboard blackboard)
        {
            if (this.behaviours != null)
            {
                for (int i = 0, count = this.behaviours.Count; i < count; i++)
                {
                    if (this.behaviours[i] is IStartBehaviour behaviour)
                    {
                        behaviour.OnStart(blackboard);
                    }
                }
            }
        }

        public void OnStop(IBlackboard blackboard)
        {
            if (this.behaviours != null)
            {
                for (int i = 0, count = this.behaviours.Count; i < count; i++)
                {
                    if (this.behaviours[i] is IStopBehaviour behaviour)
                    {
                        behaviour.OnStop(blackboard);
                    }
                }
            }
        }

        public void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (this.updateBehaviours.Count == 0)
            {
                return;
            }

            for (int i = 0, count = this.updateBehaviours.Count; i < count; i++)
            {
                IUpdateBehaviour logic = this.updateBehaviours[i];
                logic.OnUpdate(blackboard, deltaTime);
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.updateBehaviours = new List<IUpdateBehaviour>();
            
            if (this.behaviours == null)
            {
                return;
            }
            
            for (int i = 0, count = this.behaviours.Count; i < count; i++)
            {
                if (this.behaviours[i] is IUpdateBehaviour updateBehaviour)
                {
                    this.updateBehaviours.Add(updateBehaviour);
                }
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }
    }
}