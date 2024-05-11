using Atomic.Elements;
using UnityEngine;

namespace Sample
{
    public sealed class JumpController
    {
        private readonly IAtomicAction jumpAction;

        public JumpController(IAtomicAction jumpAction)
        {
            this.jumpAction = jumpAction;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                this.jumpAction?.Invoke();
            }
        }
    }
}