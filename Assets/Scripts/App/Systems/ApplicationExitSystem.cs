using System;
using Modules.Contexts;
using UnityEngine;

namespace SampleGame.App
{
    [Serializable]
    public sealed class ApplicationExitSystem : IUpdateSystem
    {
        public void Update(IContext context, float deltaTime)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                ApplicationCase.Exit();
            }
        }
    }
}