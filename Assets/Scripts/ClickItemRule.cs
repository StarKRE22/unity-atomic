using System;
using Atomic.UI;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    [Serializable]
    public sealed class ClickItemRule : IShow, IHide, IUpdate
    {
        [SerializeField]
        private Button button;

        public void OnShow(IView view)
        {
            view.GetButton().onClick.AddListener(this.OnClicked);
        }

        public void OnHide(IView view)
        {
            view.GetButton().onClick.RemoveListener(this.OnClicked);
        }

        private void OnClicked()
        {
            Debug.Log("AAAA");
        }

        public void OnUpdate(IView view, float deltaTime)
        {
            Sprite sprite = view.GetSprite();
        }
    }
}