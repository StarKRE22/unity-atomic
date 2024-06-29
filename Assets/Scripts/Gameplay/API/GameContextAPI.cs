using Atomic.Contexts;
using Modules.GameCycles;
using Modules.Inputs;
using UnityEngine;

namespace SampleGame
{
    //Codegen
    public static class GameContextAPI
    {
        public const int MoveInput = 1; 
        public const int Character = 2; 
        public const int CameraConfig = 3; 
        public const int PlayerCamera = 4; 
        public const int GameCycle = 5; 
        
        public static void AddMoveInput(this IContext context, MoveInput moveInput)
        {
            context.AddValue(MoveInput, moveInput);
        }

        public static MoveInput GetMoveInput(this IContext context)
        {
            return context.GetValue<MoveInput>(MoveInput);
        }

        public static void AddCharacter(this IContext context, ICharacter character)
        {
            context.AddValue(Character, character);
        }

        public static ICharacter GetCharacter(this IContext context)
        {
            return context.GetValue<ICharacter>(Character);
        }
        
        public static void AddCameraConfig(this IContext context, CameraConfig cameraConfig)
        {
            context.AddValue(CameraConfig, cameraConfig);
        }

        public static CameraConfig GetCameraConfig(this IContext context)
        {
            return context.GetValue<CameraConfig>(CameraConfig);
        }
        
        public static void AddPlayerCamera(this IContext context, Camera camera)
        {
            context.AddValue(PlayerCamera, camera);
        }
        
        public static Camera GetPlayerCamera(this IContext context)
        {
            return context.GetValue<Camera>(PlayerCamera);
        }
        
        public static GameCycle GetGameCycle(this IContext context)
        {
            return context.GetValue<GameCycle>(GameCycle);
        }
        
        public static void AddGameCycle(this IContext context, GameCycle gameCycle)
        {
            context.AddValue(GameCycle, gameCycle);
        }
    }
}