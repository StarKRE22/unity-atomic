using System;
using Modules.Contexts;
using UnityEngine;

namespace SampleGame.UI
{
    [Serializable]
    public sealed class GamePresentersInstaller : IContextInstaller
    {
        [SerializeField]
        private StartScreenPresenter startScreenPresenter;
        
        [SerializeField]
        private FinishScreenPresenter finishScreenPresenter;
        
        public void Install(IContext context)
        {
        }
    }
}