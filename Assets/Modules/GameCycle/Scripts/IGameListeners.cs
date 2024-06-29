namespace Modules.Gameplay
{
    public interface IGameListener //Marker
    {
    }

    public interface IGameStartListener : IGameListener
    {
        void OnStartGame();
    }

    public interface IGamePauseListener : IGameListener
    {
        void OnPauseGame();
    }

    public interface IGameResumeListener : IGameListener
    {
        void OnResumeGame();
    }

    public interface IGameFinishListener : IGameListener
    {
        void OnFinishGame();
    }

    public interface IGameTickable : IGameListener
    {
        void Tick(float deltaTime);
    }

    public interface IGameFixedTickable : IGameListener
    {
        void FixedTick(float deltaTime);
    }
    
    public interface IGameLateTickable : IGameListener
    {
        void LateTick(float deltaTime);
    }
}