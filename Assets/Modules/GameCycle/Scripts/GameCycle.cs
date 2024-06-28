using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.GameCycles
{
    public sealed class GameCycle
    {
        public event Action OnGameStarted;
        public event Action OnGamePaused;
        public event Action OnGameResumed;
        public event Action OnGameFinished;
        
        public GameState State { get; private set; }

        private readonly List<IGameListener> listeners = new();

        public void AddListener(IGameListener listener)
        {
            this.listeners.Add(listener);
        }

        public void RemoveListener(IGameListener listener)
        {
            this.listeners.Remove(listener);
        }

        public void StartGame()
        {
            if (this.State != GameState.OFF)
            {
                return;
            }

            foreach (var it in this.listeners)
            {
                if (it is IGameStartListener listener)
                {
                    listener.OnStartGame();
                }
            }
            
            this.State = GameState.PLAY;
            // this.signalBus.Fire<GameStartEvent>();
            this.OnGameStarted?.Invoke();
            Debug.Log("Game Started");
        }

        public void PauseGame()
        {
            if (this.State != GameState.PLAY)
            {
                return;
            }
            
            foreach (var it in this.listeners)
            {
                if (it is IGamePauseListener listener)
                {
                    listener.OnPauseGame();
                }
            }
            
            this.State = GameState.PAUSE;
            this.OnGamePaused?.Invoke();
            Debug.Log("Game Paused");
        }
        
        public void ResumeGame()
        {
            if (this.State != GameState.PAUSE)
            {
                return;
            }
            
            foreach (var it in this.listeners)
            {
                if (it is IGameResumeListener listener)
                {
                    listener.OnResumeGame();
                }
            }
            
            this.State = GameState.PLAY;
            this.OnGameResumed?.Invoke();
            Debug.Log("Game Resumed");
        }
        
        public void FinishGame()
        {
            if (this.State is not (GameState.PAUSE or GameState.PLAY))
            {
                return;
            }
            
            foreach (var it in this.listeners)
            {
                if (it is IGameFinishListener listener)
                {
                    listener.OnFinishGame();
                }
            }

            this.State = GameState.FINISH;
            this.OnGameFinished?.Invoke();
            Debug.Log("Game Finished");
        }
    }
}