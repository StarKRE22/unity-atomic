// using System;
// using System.Collections.Generic;
// using Atomic.Objects;
// using Sirenix.OdinInspector;
// using UnityEngine;
// using UnityEngine.Events;
//
// namespace Atomic.UI
// {
//     [AddComponentMenu("Atomic/UI/View")]
//     public sealed class GUIObject : MonoBehaviour
//     {
//         public event Action<IView> OnShown;
//         public event Action<IView> OnHidden;
//
//         public bool IsShown
//         {
//             get
//             {
//                 return this.enabled &&
//                        this.gameObject.activeSelf &&
//                        this.gameObject.activeInHierarchy;
//             }
//         }
//
//         [Space]
//         [SerializeField]
//         private UnityEvent showEvent;
//
//         [SerializeField]
//         private UnityEvent hideEvent;
//
//         private readonly List<IViewListener> listeners = new();
//
//         public void Show()
//         {
//             this.enabled = true;
//             this.gameObject.SetActive(true);
//         }
//
//         public void Hide()
//         {
//             this.enabled = false;
//             this.gameObject.SetActive(false);
//         }
//
//         public override bool AddListener(IViewListener listener)
//         {
//             if (this.listeners.Contains(listener))
//             {
//                 return false;
//             }
//
//             this.listeners.Add(listener);
//             return true;
//         }
//
//         public override bool RemoveListener(IViewListener listener)
//         {
//             return this.listeners.Remove(listener);
//         }
//
//         protected virtual void OnEnable()
//         {
//             for (int i = 0, count = this.listeners.Count; i < count; i++)
//             {
//                 IViewListener listener = this.listeners[i];
//                 listener.OnShow(this);
//             }
//
//             this.showEvent.Invoke();
//             this.OnShown?.Invoke(this);
//         }
//
//         protected virtual void OnDisable()
//         {
//             for (int i = 0, count = this.listeners.Count; i < count; i++)
//             {
//                 IViewListener listener = this.listeners[i];
//                 listener.OnHide(this);
//             }
//
//             this.hideEvent?.Invoke();
//             this.OnHidden?.Invoke(this);
//         }
//
//
//         #region Main
//
//         public event Action<IView> OnShown;
//         public event Action<IView> OnHidden;
//
//         [ShowInInspector, ReadOnly]
//         public string Name
//         {
//             get { return this.name; }
//             set { this.name = value; }
//         }
//
//         [ShowInInspector, ReadOnly]
//         public bool IsVisible
//         {
//             get { return this.enabled; }
//             set
//             {
//                 if (this.enabled == value)
//                 {
//                     return;
//                 }
//
//                 this.enabled = value;
//
//                 if (value)
//                 {
//                     this.OnShown?.Invoke(this);
//                 }
//                 else
//                 {
//                     this.OnHidden?.Invoke(this);
//                 }
//             }
//         }
//
//         #endregion
//
//         #region Values
//
//         [Serializable]
//         private struct Data
//         {
//             public string id;
//             public Component value;
//         }
//
//         [SerializeField]
//         private Data[] values;
//
//         public T GetValue<T>(string id) where T : Component
//         {
//             for (int i = 0, count = this.values.Length; i < count; i++)
//             {
//                 Data data = this.values[i];
//                 if (data.id == id)
//                 {
//                     return data.value as T;
//                 }
//             }
//
//             return null;
//         }
//
//         public bool TryGetValue<T>(string id, out T value) where T : Component
//         {
//             for (int i = 0, count = this.values.Length; i < count; i++)
//             {
//                 Data data = this.values[i];
//                 if (data.id == id)
//                 {
//                     value = data.value as T;
//                     return true;
//                 }
//             }
//
//             value = default;
//             return false;
//         }
//
//         #endregion
//
//         #region Logic
//
//         [SerializeReference]
//         private ILogic[] logics;
//
//         #endregion
//     }
// }