// using UnityEngine;
//
// namespace AI.GOAP
// {
//     [AddComponentMenu("AI/GOAP/Goal Oriented Looper")]
//     [RequireComponent(typeof(GoalOrientedAgent))]
//     [DisallowMultipleComponent]
//     public sealed class GoalOrientedLooper : MonoBehaviour
//     {
//         private GoalOrientedAgent agent;
//
//         private void Awake()
//         {
//             this.agent = this.GetComponent<GoalOrientedAgent>();
//         }
//
//         private void FixedUpdate()
//         {
//             if (!this.agent.IsPlaying)
//             {
//                 this.agent.Play();
//             }
//         }
//     }
// }