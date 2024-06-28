using Atomic.Contexts;
using UnityEngine;

namespace SampleGame
{
    public sealed class CameraFollower : ILateUpdateSystem
    {
        [Inject(GameContextAPI.Character)]
        private ICharacter character;
        
        [Inject(GameContextAPI.PlayerCamera)]
        private Camera camera;
        
        [Inject(GameContextAPI.CameraConfig)]
        private CameraConfig cameraConfig;

        public void LateUpdate(IContext context, float deltaTime)
        {
            Vector3 cameraPosition = this.character.GetPosition() + this.cameraConfig.cameraOffset;
            this.camera.transform.position = cameraPosition;
        }
    }
}