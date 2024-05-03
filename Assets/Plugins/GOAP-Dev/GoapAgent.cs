using System;
using System.Collections.Generic;
using System.Linq;
using AIModule;
using JetBrains.Annotations;
using UnityEngine;

namespace GOAPModule
{
    [AddComponentMenu("GOAP/Goal Oriented Agent")]
    [DisallowMultipleComponent]
    public class GoalOrientedAgent : MonoBehaviour
    {
        public event Action OnStarted;
        public event Action OnFailed;
        public event Action OnCanceled;
        public event Action OnCompleted;

        [SerializeField]
        private Blackboard blackboard;

        [SerializeField]
        private GoapPlanner planner;

        [SerializeField]
        private List<GoapGoal> goals;

        [SerializeField]
        private List<GoapAction> actions;

        [SerializeField]
        private List<GoapSensor> sensors;

        private Dictionary<ushort, bool> _worldState = new();

        private GoapGoal _currentGoal;
        private List<GoapAction> _currentPlan;
        private int _currentActionIndex;

        private void Awake()
        {
            this.actions = this.actions.Select(Instantiate).ToList();
        }

        private void FixedUpdate()
        {
            if (_currentPlan == null)
            {
                this.GeneratePlan();
            }
            else
            {
                this.ExecutePlan(Time.fixedDeltaTime);
            }
        }

        #region GeneratePlan

        private void GeneratePlan()
        {
            IEnumerable<GoapAction> actions = this.actions.Where(it => it.IsValid(this.blackboard));
            if (!actions.Any())
            {
                Debug.LogWarning("Can't generate plan: no valid actions!");
                return;
            }

            IEnumerable<GoapGoal> goals = this.goals
                .Where(it => it.IsValid(this.blackboard))
                .OrderByDescending(it => it.GetPriority(this.blackboard));

            if (!goals.Any())
            {
                Debug.LogWarning("Can't generate plan: no valid goals!");
            }

            IReadOnlyDictionary<ushort, bool> worldState = this.GenerateWorldState();

            foreach (var goal in goals)
            {
                if (!this.planner.MakePlan(worldState, goal.postConditions, actions, out List<GoapAction> plan))
                {
                    Debug.LogWarning($"Can't generate a plan for goal {goal.name}!");
                    continue;
                }

                if (plan.Count <= 0)
                {
                    Debug.LogWarning($"Plan for goal {goal.name} is empty!");
                    continue;
                }

                _currentGoal = goal;
                _currentPlan = plan;
                _currentActionIndex = 0;
                
                OnStarted?.Invoke();
                return;
            }
        }

        private IReadOnlyDictionary<ushort, bool> GenerateWorldState()
        {
            _worldState.Clear();
            
            for (int i = 0, count = this.sensors.Count; i < count; i++)
            {
                GoapSensor sensor = this.sensors[i];
                sensor.GenerateFacts(this.blackboard, _worldState);
            }

            return _worldState;
        }

        #endregion

        #region ExecutePlan

        private void ExecutePlan(float deltaTime)
        {
            GoapAction currentAction = _currentPlan[_currentActionIndex];
            GoapAction.State state = currentAction.Run(this.blackboard, deltaTime);

            if (state == GoapAction.State.RUNNING)
            {
                return;
            }

            if (state == GoapAction.State.FAILURE)
            {
                this.OnFailure();
            }

            //Success:
            if (_currentActionIndex == _currentPlan.Count - 1)
            {
                this.OnSuccess();
            }
            else
            {
                _currentActionIndex++;
            }
        }

        private void OnFailure()
        {
            _currentPlan = null;
            _currentActionIndex = 0;
            this.OnFailed?.Invoke();
        }

        private void OnSuccess()
        {
            _currentPlan = null;
            _currentActionIndex = 0;
            this.OnCompleted?.Invoke();
        }

        #endregion

        public void Cancel()
        {
            if (_currentPlan != null && _currentActionIndex < this._currentPlan.Count)
            {
                GoapAction action = _currentPlan[this._currentActionIndex];
                action.Cancel(this.blackboard);
            }

            _currentPlan = null;
            _currentActionIndex = 0;
            this.OnCanceled?.Invoke();
        }

        public IReadOnlyList<GoapGoal> GetGoals()
        {
            return this.goals;
        }

        public void AddGoal(GoapGoal goal)
        {
            this.goals.Add(goal);
        }

        public void RemoveGoal(GoapGoal goal)
        {
            this.goals.Remove(goal);
        }
        
        public IReadOnlyList<GoapAction> GetActions()
        {
            return this.actions;
        }

        public void AddAction(GoapAction action)
        {
            this.actions.Add(Instantiate(action));
        }

        public void RemoveAction(GoapAction action)
        {
            this.actions.Remove(action);
            Destroy(action);
        }

        [CanBeNull]
        public GoapGoal GetCurrentGoal()
        {
            return _currentGoal;
        }

        [CanBeNull]
        public IReadOnlyList<GoapAction> GetCurrentPlan()
        {
            return _currentPlan;
        }
    }
}