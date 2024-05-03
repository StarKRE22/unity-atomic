// using System.Collections;
// using UnityEngine;
//
// namespace AI.GOAP
// {
//     [AddComponentMenu("AI/GOAP/Goal Oriented Restarter")]
//     [RequireComponent(typeof(GoalOrientedAgent))]
//     [DisallowMultipleComponent]
//     public class GoalOrientedRestarter : MonoBehaviour
//     {
//         [Space]
//         [SerializeField]
//         private bool playOnStart = true;
//
//         [Space]
//         [SerializeField]
//         private float minScanPeriod = 0.1f;
//
//         [SerializeField]
//         private float maxScanPeriod = 0.2f;
//
//         private GoalOrientedAgent agent;
//
//         private Coroutine coroutine;
//
//         private void Awake()
//         {
//             this.agent = this.GetComponent<GoalOrientedAgent>();
//         }
//
//         private void Start()
//         {
//             if (this.playOnStart)
//             {
//                 this.Play();
//             }
//         }
//
//         public void Play()
//         {
//             if (this.coroutine == null)
//             {
//                 this.coroutine = this.StartCoroutine(this.CheckState());
//             }
//         }
//
//         public void Stop()
//         {
//             if (this.coroutine != null)
//             {
//                 this.StopCoroutine(this.coroutine);
//                 this.coroutine = null;
//             }
//         }
//
//         private IEnumerator CheckState()
//         {
//             while (true)
//             {
//                 var period = Random.Range(this.minScanPeriod, this.maxScanPeriod);
//                 yield return new WaitForSeconds(period);
//                 this.agent.Replay();
//             }
//         }
//     }
// }