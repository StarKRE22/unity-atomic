using System;
using Modules.Contexts;
using Modules.GameCycles;
using Modules.Inputs;
using UnityEngine;

namespace SampleGame
{
    //Codegen
    public static class ContextAPI
    {
        public const int MoveInput = 1; 
        public const int Character = 2; 
        public const int CameraConfig = 3; 
        public const int PlayerCamera = 4; 
        public const int GameCycle = 5; 
        
        public static void AddMoveInput(this IContext context, MoveInput moveInput)
        {
            context.AddData(MoveInput, moveInput);
        }

        public static MoveInput GetMoveInput(this IContext context)
        {
            return context.GetData<MoveInput>(MoveInput);
        }

        public static void AddCharacter(this IContext context, ICharacter character)
        {
            context.AddData(Character, character);
        }

        public static ICharacter GetCharacter(this IContext context)
        {
            return context.GetData<ICharacter>(Character);
        }
        
        public static void AddCameraConfig(this IContext context, CameraConfig cameraConfig)
        {
            context.AddData(CameraConfig, cameraConfig);
        }

        public static CameraConfig GetCameraConfig(this IContext context)
        {
            return context.GetData<CameraConfig>(CameraConfig);
        }
        
        public static void AddPlayerCamera(this IContext context, Camera camera)
        {
            context.AddData(PlayerCamera, camera);
        }
        
        public static Camera GetPlayerCamera(this IContext context)
        {
            return context.GetData<Camera>(PlayerCamera);
        }
        
        public static GameCycle GetGameCycle(this IContext context)
        {
            return context.GetData<GameCycle>(GameCycle);
        }
        
        public static void AddGameCycle(this IContext context, GameCycle gameCycle)
        {
            context.AddData(GameCycle, gameCycle);
        }
    }
}