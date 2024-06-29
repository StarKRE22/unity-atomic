using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;

namespace Modules.Gameplay
{
    public sealed class GameCycle
    {
        public event Action OnStarted;
        public event Action OnPaused;
        public event Action OnResumed;
        public event Action OnFinished;

        public event Action<float> OnTick;
        public event Action<float> OnFixedTick;
        public event Action<float> OnLateTick;

        [ShowInInspector, ReadOnly]
        public GameState State => this.state;

        private readonly HashSet<IGameListener> listeners = new();
        private GameState state = GameState.OFF;

        private readonly List<IGameTickable> tickables = new();
        private readonly List<IGameFixedTickable> fixedTickables = new();
        private readonly List<IGameLateTickable> lateTickables = new();

        private readonly List<IGameTickable> tickCache = new();
        private readonly List<IGameFixedTickable> fixedTickCache = new();
        private readonly List<IGameLateTickable> lateTickCache = new();

        public bool AddListener(IGameListener listener)
        {
            if (listener == null)
            {
                return false;
            }

            if (!this.listeners.Add(listener))
            {
                return false;
            }

            if (listener is IGameTickable tickable)
            {
                this.tickables.Add(tickable);
            }

            if (listener is IGameFixedTickable fixedTickable)
            {
                this.fixedTickables.Add(fixedTickable);
            }

            if (listener is IGameLateTickable lateTickable)
            {
                this.lateTickables.Add(lateTickable);
            }

            return true;
        }

        public bool DelListener(IGameListener listener)
        {
            if (listener == null)
            {
                return false;
            }

            if (!this.listeners.Remove(listener))
            {
                return false;
            }

            if (listener is IGameTickable tickable)
            {
                this.tickables.Remove(tickable);
            }

            if (listener is IGameFixedTickable fixedTickable)
            {
                this.fixedTickables.Remove(fixedTickable);
            }

            if (listener is IGameLateTickable lateTickable)
            {
                this.lateTickables.Remove(lateTickable);
            }

            return true;
        }

        [Title("Methods")]
        [Button, HideInEditorMode]
        public void Start()
        {
            if (this.state != GameState.OFF)
            {
                return;
            }

            foreach (IGameListener listener in this.listeners)
            {
                if (listener is IGameStartListener startListener)
                {
                    startListener.OnStartGame();
                }
            }

            this.state = GameState.PLAY;
            this.OnStarted?.Invoke();
        }

        [Button, HideInEditorMode]
        public void Pause()
        {
            if (this.state != GameState.PLAY)
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

            this.state = GameState.PAUSE;
            this.OnPaused?.Invoke();
        }

        [Button, HideInEditorMode]
        public void Resume()
        {
            if (this.state != GameState.PAUSE)
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

            this.state = GameState.PLAY;
            this.OnResumed?.Invoke();
        }

        [Button, HideInEditorMode]
        public void Finish()
        {
            if (this.state is not (GameState.PAUSE or GameState.PLAY))
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

            this.state = GameState.FINISH;
            this.OnFinished?.Invoke();
        }

        public bool Tick(float deltaTime)
        {
            if (this.state != GameState.PLAY)
            {
                return false;
            }

            int count = this.tickables.Count;
            if (count > 0)
            {
                this.tickCache.Clear();
                this.tickCache.AddRange(this.tickables);

                for (int i = 0; i < count; i++)
                {
                    IGameTickable tickable = this.tickCache[i];
                    tickable.Tick(deltaTime);
                }
            }

            this.OnTick?.Invoke(deltaTime);
            return true;
        }

        public bool FixedTick(float deltaTime)
        {
            if (this.state != GameState.PLAY)
            {
                return false;
            }

            int count = this.fixedTickables.Count;
            if (count > 0)
            {
                this.fixedTickCache.Clear();
                this.fixedTickCache.AddRange(this.fixedTickables);

                for (int i = 0; i < count; i++)
                {
                    IGameFixedTickable tickable = this.fixedTickCache[i];
                    tickable.FixedTick(deltaTime);
                }
            }

            this.OnFixedTick?.Invoke(deltaTime);
            return true;
        }

        public bool LateTick(float deltaTime)
        {
            if (this.state != GameState.PLAY)
            {
                return false;
            }

            int count = this.lateTickables.Count;
            if (count != 0)
            {
                this.lateTickCache.Clear();
                this.lateTickCache.AddRange(this.lateTickables);

                for (int i = 0; i < count; i++)
                {
                    IGameLateTickable tickable = this.lateTickCache[i];
                    tickable.LateTick(deltaTime);
                }
            }

            this.OnLateTick?.Invoke(deltaTime);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsOff()
        {
            return this.state == GameState.OFF;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPlaying()
        {
            return this.state == GameState.PLAY;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsPaused()
        {
            return this.state == GameState.PAUSE;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsFinished()
        {
            return this.state == GameState.FINISH;
        }
    }
}


// if (this.state is > GameState.OFF and < GameState.FINISH &&
//     listener is IGameFinishListener finishListener)
// {
//     finishListener.OnFinishGame();
// }


// if (this.state is > GameState.OFF and < GameState.FINISH &&
//     listener is IGameStartListener startListener)
// {
//     startListener.OnStartGame();
// }
//
// if (this.state == GameState.PAUSE &&
//     listener is IGamePauseListener pauseListener)
// {
//     pauseListener.OnPauseGame();
// }