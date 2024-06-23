using Sirenix.OdinInspector;
using UnityEngine;

// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace Atomic.Elements
{
    [AddComponentMenu("Atomic/Elements/Scene Action")]
    public class SceneAction : SceneActionBase
    {
        [SerializeReference]
        protected IAtomicAction[] actions = default;

        [HideInEditorMode]
        [GUIColor(0, 1, 0)]
        [Button]
        public override void Invoke()
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