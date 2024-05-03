// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using Sirenix.OdinInspector;
// using UnityEngine;
//
// // ReSharper disable EventNeverSubscribedTo.Global
//
// namespace AI.GOAP
// {
//     [AddComponentMenu("AI/GOAP/Goal Oriented Agent")]
//     [DisallowMultipleComponent]
//     public class GoalOrientedAgent : MonoBehaviour, IActor.Callback
//     {
//         public event Action OnStarted;
//         public event Action OnFailed;
//         public event Action OnCanceled;
//         public event Action OnCompleted;
//
//         [SerializeField, PropertyOrder(-11), HideInPlayMode]
//         private PlannerMode plannerMode;
//
//         private IPlanner planner;
//
//         [SerializeField, Space, PropertyOrder(-9), HideInPlayMode]
//         private Goal[] goals;
//
//         [SerializeField, Space, PropertyOrder(-8), HideInPlayMode]
//         private Actor[] actions;
//
//         [SerializeField, Space, PropertyOrder(-7), HideInPlayMode]
//         private FactInspector[] factInspectors;
//
//         [Title("Debug")]
//         [ShowInInspector, ReadOnly, PropertySpace, PropertyOrder(-6)]
//         public bool IsPlaying
//         {
//             get { return this.currentPlan != null; }
//         }
//
//         [ShowInInspector, ReadOnly, PropertyOrder(-5)]
//         public IGoal CurrentGoal
//         {
//             get { return this.currentGoal; }
//         }
//
//         public FactState WorldState
//         {
//             get { return this.GenerateWorldState(); }
//         }
//
//         public IEnumerable<IGoal> Goals
//         {
//             get { return this.goals; }
//         }
//
//         public IEnumerable<IActor> Actions
//         {
//             get { return this.actions; }
//         }
//         
//         private IGoal currentGoal;
//
//         [ShowInInspector, ReadOnly, PropertyOrder(-5)]
//         private List<IActor> currentPlan;
//
//         [ShowInInspector, ReadOnly, PropertyOrder(-4)]
//         private int actionIndex;
//
//         private void Awake()
//         {
//             this.planner = PlannerFactory.CreatePlanner(this.plannerMode);
//         }
//
//         public void Play()
//         {
//             Goal goal = this.goals
//                 .Where(it => it.IsValid())
//                 .OrderByDescending(it => it.EvaluatePriority())
//                 .FirstOrDefault();
//
//             if (goal == null)
//             {
//                 Debug.LogWarning("Can't play: no valid goals!");
//                 return;
//             }
//
//             IActor[] actions = this.actions
//                 .Where(it => it.IsValid())
//                 .ToArray<IActor>();
//
//             if (actions.Length <= 0)
//             {
//                 Debug.LogWarning("Can't play: no valid actions!");
//                 return;
//             }
//
//             var worldState = this.GenerateWorldState();
//
//             if (!this.planner.MakePlan(worldState, goal.ResultState, actions, out List<IActor> plan))
//             {
//                 Debug.LogWarning($"Can't make a plan for goal {goal.name}!");
//                 return;
//             }
//
//             if (plan.Count <= 0)
//             {
//                 Debug.LogWarning($"Plan for goal {goal.name} is empty!");
//                 return;
//             }
//
//             this.currentGoal = goal;
//             this.currentPlan = plan;
//
//             this.actionIndex = 0;
//             this.OnStarted?.Invoke();
//
//             this.PlayAction();
//         }
//
//         public void Cancel()
//         {
//             this.StopAllCoroutines();
//             
//             if (this.currentPlan != null && 
//                 this.actionIndex < this.currentPlan.Count)
//             {
//                 this.currentPlan[this.actionIndex].Cancel();
//             }
//
//             this.currentPlan = null;
//             this.actionIndex = 0;
//             this.OnCancel();
//         }
//
//         public void Replay()
//         {
//             this.Cancel();
//             this.Play();
//         }
//         
//         private void PlayAction()
//         {
//             var action = this.currentPlan[this.actionIndex];
//             if (!action.IsValid())
//             {
//                 this.Fail();
//                 return;
//             }
//
//             if (!action.RequiredState.EqualsTo(this.WorldState))
//             {
//                 this.Fail();
//                 return;
//             }
//
//             action.Play(callback: this);
//         }
//
//         void IActor.Callback.Invoke(IActor action, bool success)
//         {
//             if (!success)
//             {
//                 this.Fail();
//                 return;
//             }
//
//             if (!action.ResultState.EqualsTo(this.WorldState))
//             {
//                 this.Fail();
//                 return;
//             }
//
//             var planCompleted = this.actionIndex + 1 >= this.currentPlan.Count;
//             if (planCompleted)
//             {
//                 this.Complete();
//                 return;
//             }
//
//             this.actionIndex++;
//             this.StartCoroutine(this.PlayNextAction());
//         }
//
//         private IEnumerator PlayNextAction()
//         {
//             yield return new WaitForFixedUpdate();
//             this.PlayAction();
//         }
//
//         private void Fail()
//         {
//             this.currentPlan = null;
//             this.actionIndex = 0;
//             this.OnFail();
//         }
//
//         private void Complete()
//         {
//             this.currentPlan = null;
//             this.actionIndex = 0;
//             this.OnComplete();
//         }
//         
//         private FactState GenerateWorldState()
//         {
//             var worldState = new FactState();
//             
//             for (int i = 0, count = this.factInspectors.Length; i < count; i++)
//             {
//                 var inspector = this.factInspectors[i];
//                 inspector.PopulateFacts(worldState);
//             }
//
//             return worldState;
//         }
//
//         protected virtual void OnFail()
//         {
//             this.OnFailed?.Invoke();
//         }
//
//         protected virtual void OnComplete()
//         {
//             this.OnCompleted?.Invoke();
//         }
//
//         protected virtual void OnCancel()
//         {
//             this.OnCanceled?.Invoke();
//         }
//     }
// }