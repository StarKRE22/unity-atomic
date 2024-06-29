/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Contexts;
using System.Runtime.CompilerServices;
using ;

namespace SampleGame
{
	public static class GameplayAPI
	{
		///Keys
		public const int MoveInput = 1; // MoveInput
		public const int Character = 2; // ICharacter


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
    }
}
