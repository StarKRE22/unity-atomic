using System;
using Atomic.Contexts;
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
            context.AddPlayerCamera(this.camera);
            context.AddSystem(new CameraFollower(this.cameraConfig));
        }
    }
}