using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.AI
{
    [Serializable]
    public sealed class StateMachine : State, ISerializationCallbackReceiver
    {
        [Header("States")]
        [ValueDropdown(nameof(DrawStateNames))]
        [SerializeField]
        private int currentState;

#if UNITY_EDITOR
        [ListDrawerSettings(OnBeginListElementGUI = nameof(DrawStateLabel))]
#endif
        //TODO: Реогранизовать список в массив. Чтобы индекс был айдишником!
        [SerializeReference]
        private IState[] states = Array.Empty<IState>();

        [Header("Transitions")]
#if UNITY_EDITOR
        [ListDrawerSettings(OnBeginListElementGUI = nameof(DrawTransitionLabel))]
#endif
        [SerializeField]
        private StateTransition[] transitions = Array.Empty<StateTransition>();

        private Dictionary<int, List<StateTransition>> transitionMap;

        public override void OnEnter(IBlackboard blackboard)
        {
            base.OnEnter(blackboard);
            
            IState currentState = this.states[this.currentState];
            currentState.OnEnter(blackboard);
        }

        public override void OnExit(IBlackboard blackboard)
        {
            base.OnExit(blackboard);
            
            IState currentState = this.states[this.currentState];
            currentState.OnExit(blackboard);
        }

        public override void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            base.OnUpdate(blackboard, deltaTime);
            
            if (this.transitionMap.Count > 0)
            {
                //Switch to next state:
                List<StateTransition> transitions = this.transitionMap[this.currentState];

                for (int i = 0, count = transitions.Count; i < count; i++)
                {
                    StateTransition transition = transitions[i];
                    if (transition.Check(blackboard))
                    {
                        IState previousState = this.states[this.currentState];
                        previousState.OnExit(blackboard);

                        transition.Perform(blackboard);

                        this.currentState = transition.targetState;

                        IState nextState = this.states[this.currentState];
                        nextState.OnEnter(blackboard);
                        break;
                    }
                }
            }

            //Update current state:
            IState currentState = this.states[this.currentState];
            currentState.OnUpdate(blackboard, deltaTime);
        }


        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            //Init transitions:
            this.transitionMap = new Dictionary<int, List<StateTransition>>();
            foreach (var transition in this.transitions)
            {
                int sourceState = transition.sourceState;
                if (!this.transitionMap.TryGetValue(sourceState, out List<StateTransition> transitions))
                {
                    transitions = new List<StateTransition>();
                    this.transitionMap.Add(sourceState, transitions);
                }

                transitions.Add(transition);
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }

#if UNITY_EDITOR
        private void DrawStateLabel(int index)
        {
            if (this.states == null)
            {
                return;
            }

            IState state = this.states[index];

            string label = string.IsNullOrWhiteSpace(state.Name)
                ? $"{index + 1}. Undefined state"
                : $"{index + 1}. {state.Name}";

            GUILayout.Space(4);

            Color color = GUI.color;
            GUI.color = Color.green;
            GUILayout.Label(label);
            GUI.color = color;
        }

        private void DrawTransitionLabel(int index)
        {
            StateTransition transition = this.transitions[index];
            transition.editor_stateNamesDebug = this.DrawStateNames();

            string sourceName = this.states[transition.sourceState].Name;
            string targetName = this.states[transition.targetState].Name;

            GUILayout.Space(4);
            Color color = GUI.color;
            GUI.color = Color.yellow;
            GUILayout.Label($"{sourceName} => {targetName}");
            GUI.color = color;
        }

        private ValueDropdownList<int> DrawStateNames()
        {
            var result = new ValueDropdownList<int>();

            if (this.states == null)
            {
                return result;
            }

            for (int i = 0, count = this.states.Length; i < count; i++)
            {
                var state = this.states[i];
                if (state != null)
                {
                    result.Add(new ValueDropdownItem<int>(state.Name, i));
                }
            }

            return result;
        }
#endif
    }
}