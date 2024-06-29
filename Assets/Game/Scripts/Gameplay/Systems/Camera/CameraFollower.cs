using Atomic.Contexts;
using Modules.Gameplay;
using UnityEngine;

namespace SampleGame
{
    public sealed class CameraFollower : IInitSystem, IDisposeSystem, IGameLateTickable
    {
        private readonly CameraConfig cameraConfig;
        
        private ICharacter character;
        private Camera camera;
        private GameCycle gameCycle;
        
        public CameraFollower(CameraConfig cameraConfig)
        {
            this.cameraConfig = cameraConfig;
        }

        public void Init(IContext context)
        {
            this.character = context.GetCharacter();
            this.camera = context.GetPlayerCamera();
            
            this.gameCycle = context.ResolveGameCycle();
            this.gameCycle.AddListener(this);
        }
        
        public void Dispose(IContext context)
        {
            this.gameCycle.DelListener(this);
        }

        public void LateTick(float deltaTime)
        {
            Vector3 cameraPosition = this.character.GetPosition() + this.cameraConfig.cameraOffset;
            this.camera.transform.position = cameraPosition;
        }
    }
}