using System;
using Modules.Contexts;
using UnityEngine;

namespace SampleGame
{
    [Serializable]
    public sealed class CameraInstaller : IContextInstaller
    {
        [SerializeField]
        private CameraConfig cameraConfig;

        [SerializeField]
        private Camera camera;
        
        public void Install(IContext context)
        {
            context.AddCameraConfig(this.cameraConfig);
            context.AddPlayerCamera(this.camera);
            context.AddSystem<CameraFollower>();
        }
    }
}