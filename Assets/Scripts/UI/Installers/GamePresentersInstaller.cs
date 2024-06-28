using System;
using Atomic.Contexts;
using UnityEngine;

namespace SampleGame.UI
{
    [Serializable]
    public sealed class GamePresentersInstaller : IContextInstaller
    {
        [SerializeField]
        private StartScreenPresenter startScreenPresenter;
        
        [SerializeField]
        private GameScreenPresenter gameScreenPresenter;
        
        public void Install(IContext context)
        {
        }
    }
}