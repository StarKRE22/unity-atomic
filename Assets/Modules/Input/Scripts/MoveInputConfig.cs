using UnityEngine;

namespace Modules.Gameplay
{
    [CreateAssetMenu(
        fileName = "MoveInputConfig",
        menuName = "Gameplay/New MoveInputConfig"
    )]
    public sealed class MoveInputConfig : ScriptableObject
    {
        public KeyCode left;
        public KeyCode right;
        public KeyCode forward;
        public KeyCode back;
    }
}