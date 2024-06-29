/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Contexts;
using System.Runtime.CompilerServices;
using Modules.Gameplay;

namespace SampleGame
{
	public static class GameplayAPI
	{
		///Keys
		public const int MoveInput = 1; // MoveInput
		public const int Character = 2; // ICharacter
		public const int PlayerCamera = 3; // Camera
		public const int GameCycle = 4; // GameCycle


		///Extensions
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static MoveInput GetMoveInput(this IContext obj) => obj.GetValue<MoveInput>(MoveInput);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetMoveInput(this IContext obj, out MoveInput value) => obj.TryGetValue(MoveInput, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static MoveInput ResolveMoveInput(this IContext obj) => obj.ResolveValue<MoveInput>(MoveInput);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryResolveMoveInput(this IContext obj, out MoveInput value) => obj.TryResolveValue(MoveInput, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddMoveInput(this IContext obj, MoveInput value) => obj.AddValue(MoveInput, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelMoveInput(this IContext obj) => obj.DelValue(MoveInput);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetMoveInput(this IContext obj, MoveInput value) => obj.SetValue(MoveInput, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasMoveInput(this IContext obj) => obj.HasValue(MoveInput);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ICharacter GetCharacter(this IContext obj) => obj.GetValue<ICharacter>(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCharacter(this IContext obj, out ICharacter value) => obj.TryGetValue(Character, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ICharacter ResolveCharacter(this IContext obj) => obj.ResolveValue<ICharacter>(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryResolveCharacter(this IContext obj, out ICharacter value) => obj.TryResolveValue(Character, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCharacter(this IContext obj, ICharacter value) => obj.AddValue(Character, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCharacter(this IContext obj) => obj.DelValue(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCharacter(this IContext obj, ICharacter value) => obj.SetValue(Character, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCharacter(this IContext obj) => obj.HasValue(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Camera GetPlayerCamera(this IContext obj) => obj.GetValue<Camera>(PlayerCamera);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetPlayerCamera(this IContext obj, out Camera value) => obj.TryGetValue(PlayerCamera, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Camera ResolvePlayerCamera(this IContext obj) => obj.ResolveValue<Camera>(PlayerCamera);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryResolvePlayerCamera(this IContext obj, out Camera value) => obj.TryResolveValue(PlayerCamera, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddPlayerCamera(this IContext obj, Camera value) => obj.AddValue(PlayerCamera, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelPlayerCamera(this IContext obj) => obj.DelValue(PlayerCamera);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPlayerCamera(this IContext obj, Camera value) => obj.SetValue(PlayerCamera, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasPlayerCamera(this IContext obj) => obj.HasValue(PlayerCamera);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GameCycle GetGameCycle(this IContext obj) => obj.GetValue<GameCycle>(GameCycle);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetGameCycle(this IContext obj, out GameCycle value) => obj.TryGetValue(GameCycle, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static GameCycle ResolveGameCycle(this IContext obj) => obj.ResolveValue<GameCycle>(GameCycle);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryResolveGameCycle(this IContext obj, out GameCycle value) => obj.TryResolveValue(GameCycle, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddGameCycle(this IContext obj, GameCycle value) => obj.AddValue(GameCycle, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelGameCycle(this IContext obj) => obj.DelValue(GameCycle);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetGameCycle(this IContext obj, GameCycle value) => obj.SetValue(GameCycle, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasGameCycle(this IContext obj) => obj.HasValue(GameCycle);
    }
}
