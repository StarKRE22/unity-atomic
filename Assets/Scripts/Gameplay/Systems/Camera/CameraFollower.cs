using Atomic.Contexts;
using Modules.GameCycles;
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
            Debug.Log("INIT FOLLOWER");
            this.character = context.GetCharacter();
            this.camera = context.GetPlayerCamera();
            this.gameCycle = context.GetGameCycle();
            this.gameCycle.AddListener(this);
        }
        
        public void Dispose(IContext context)
        {
            this.gameCycle.DelListener(this);
        }

        public void LateTick(float deltaTime)
        {
            Debug.Log("LATE TICK");
            Vector3 cameraPosition = this.character.GetPosition() + this.cameraConfig.cameraOffset;
            this.camera.transform.position = cameraPosition;
        }
    }
}