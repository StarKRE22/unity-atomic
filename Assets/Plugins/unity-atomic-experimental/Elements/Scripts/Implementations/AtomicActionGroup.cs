using System;
using Atomic.Elements;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public sealed class AtomicActionGroup : IAtomicAction
    {
        [SerializeReference]
        private IAtomicAction[] actions;

        public AtomicActionGroup(params IAtomicAction[] actions)
        {
            this.actions = actions;
        }

        public void Invoke()
        {
            if (this.actions == null)
            {
                return;
            }

            for (int i = 0, count = this.actions.Length; i < count; i++)
            {
                IAtomicAction action = this.actions[i];
                action?.Invoke();
            }
        }
    }
}