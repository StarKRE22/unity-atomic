using Atomic.Contexts;
using UnityEngine;

namespace SampleGame
{
    public sealed class CameraFollower : IInitSystem, ILateUpdateSystem
    {
        private ICharacter character;
        private Camera camera;
        private CameraConfig cameraConfig;
        
        public void Init(IContext context)
        {
            this.character = context.GetCharacter();
            this.camera = context.GetPlayerCamera();
            this.cameraConfig = context.GetCameraConfig();
        }

        public void LateUpdate(float deltaTime)
        {
            Vector3 cameraPosition = this.character.GetPosition() + this.cameraConfig.cameraOffset;
            this.camera.transform.position = cameraPosition;
        }
    }
}