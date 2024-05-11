// using UnityEngine;
//
// namespace Atomic.Objects
// {
//     [AddComponentMenu("Atomic/Atomic Object Runner")]
//     [DisallowMultipleComponent]
//     public sealed class AtomicObjectRunner : MonoBehaviour
//     {
//         [SerializeField]
//         private AtomicObject target;
//         
//         private void Reset()
//         {
//             this.target = this.GetComponent<AtomicObject>();
//         }
//         
//         private void Awake()
//         {
//             this.target.OnAwake();
//         }
//
//         private void OnEnable()
//         {
//             this.target.Enable();
//         }
//
//         private void Update()
//         {
//             this.target.OnUpdate(Time.deltaTime);
//         }
//
//         private void FixedUpdate()
//         {
//             this.target.OnFixedUpdate(Time.fixedDeltaTime);
//         }
//
//         private void LateUpdate()
//         {
//             this.target.OnLateUpdate(Time.deltaTime);
//         }
//
// #if UNITY_EDITOR
//         private void OnDrawGizmos()
//         {
//             this.target.OnGizmosDraw();
//         }
// #endif
//         private void OnDisable()
//         {
//             this.target.Disable();
//         }
//
//         private void OnDestroy()
//         {
//             this.target.Dispose();
//         }
//
//         private void OnTriggerEnter(Collider other)
//         {
//             this.target.TriggerEnter(other);
//         }
//
//         private void OnTriggerExit(Collider other)
//         {
//             this.target.TriggerExit(other);
//         }
//
//         private void OnTriggerEnter2D(Collider2D other)
//         {
//             this.target.TriggerEnter2D(other);
//         }
//
//         private void OnTriggerExit2D(Collider2D other)
//         {
//             this.target.TriggerExit2D(other);
//         }
//
//         private void OnCollisionEnter(Collision collision)
//         {
//             this.target.CollisionEnter(collision);
//         }
//
//         private void OnCollisionExit(Collision other)
//         {
//             this.target.CollisionEnter(other);
//         }
//
//         private void OnCollisionEnter2D(Collision2D other)
//         {
//             this.target.CollisionEnter2D(other);
//         }
//
//         private void OnCollisionExit2D(Collision2D other)
//         {
//             this.target.CollisionExit2D(other);
//         }
//     }
// }