using System;
using Atomic.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public sealed class ClickItemRule : IShow, IHide, IUpdate
    {
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