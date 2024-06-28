using System;
using Modules.Contexts;
using UnityEngine;

namespace SampleGame
{
    [Serializable]
    public sealed class GameTimeDebug : IUpdateSystem
    {
        private float currentTime;

        public void Update(IContext context, float deltaTime)
        {
            this.currentTime += deltaTime;
            Debug.Log("Game Time: " + currentTime.ToString("F0"));
        }
    }
}