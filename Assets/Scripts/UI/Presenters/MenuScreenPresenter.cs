using System;
using Modules.Contexts;
using SampleGame.App;
using UnityEngine;

namespace SampleGame.UI
{
    [Serializable]
    public sealed class MenuScreenPresenter : IInitSystem, IDisposable
    {
        [SerializeField]
        private MenuScreenView menuScreen;

        public void Init(IContext context)
        {
            this.menuScreen.startButton.onClick.AddListener(GameLaunchCase.LaunchGame);
            this.menuScreen.exitButton.onClick.AddListener(ApplicationCase.Exit);
        }

        public void Dispose()
        {
            this.menuScreen.startButton.onClick.RemoveListener(GameLaunchCase.LaunchGame);
            this.menuScreen.exitButton.onClick.RemoveListener(ApplicationCase.Exit);
        }
    }
}