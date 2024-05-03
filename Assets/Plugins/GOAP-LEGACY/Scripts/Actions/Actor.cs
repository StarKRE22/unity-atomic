// using Sirenix.OdinInspector;
// using UnityEngine;
//
// namespace AI.GOAP
// {
//     public abstract class Actor : MonoBehaviour, IActor
//     {
//         public IFactState ResultState
//         {
//             get { return this.resultState; }
//         }
//
//         public IFactState RequiredState
//         {
//             get { return this.requiredState; }
//         }
//
//         [ShowInInspector, ReadOnly, PropertyOrder(-10)]
//         public bool IsPlaying
//         {
//             get { return this.isPlaying; }
//         }
//
//         [Space]
//         [SerializeField]
//         protected FactState resultState;
//
//         [SerializeField]
//         protected FactState requiredState;
//
//         private bool isPlaying;
//
//         private IActor.Callback callback;
//
//         public abstract int EvaluateCost();
//
//         public abstract bool IsValid();
//
//         public void Play(IActor.Callback callback)
//         {
//             if (this.isPlaying)
//             {
//                 return;
//             }
//
//             this.callback = callback;
//             this.isPlaying = true;
//             this.Play();
//         }
//
//         public void Cancel()
//         {
//             if (!this.isPlaying)
//             {
//                 return;
//             }
//
//             this.OnCancel();
//             this.isPlaying = false;
//             this.callback = null;
//             this.OnDispose();
//         }
//
//         protected abstract void Play();
//
//         protected virtual void Return(bool success)
//         {
//             if (!this.isPlaying)
//             {
//                 return;
//             }
//             
//             this.isPlaying = false;
//             this.OnReturn();
//             this.OnDispose();
//             this.InvokeCallback(success);
//         }
//
//         #region Callbacks
//
//         protected virtual void OnReturn()
//         {
//         }
//
//         protected virtual void OnCancel()
//         {
//         }
//
//         protected virtual void OnDispose()
//         {
//         }
//
//         #endregion
//
//         private void InvokeCallback(bool success)
//         {
//             if (this.callback == null)
//             {
//                 return;
//             }
//
//             var callback = this.callback;
//             this.callback = null;
//             callback.Invoke(this, success);
//         }
//     }
// }