using Atomic;
using UnityEngine;

namespace Game.Engine
{
    public sealed class ActionController
    {
        private readonly KeyCode keyCode;
        private readonly IAtomicAction attackAction;

        public ActionController(KeyCode keyCode, IAtomicAction attackAction)
        {
            this.keyCode = keyCode;
            this.attackAction = attackAction;
        }

        public void Update()
        {
            if (Input.GetKeyDown(this.keyCode))
            {
                this.attackAction?.Invoke();
            }
        }
    }
}