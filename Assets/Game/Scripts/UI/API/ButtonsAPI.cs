/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.UI;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

namespace SampleGame.UI
{
	public static class ButtonsAPI
	{
		///Keys
		public const int StartButton = 1; // Button
		public const int ExitButton = 2; // Button
		public const int ResumeButton = 3; // Button
		public const int FinishButton = 4; // Button


		///Extensions
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Button GetStartButton(this IView obj) => obj.GetValue<Button>(StartButton);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetStartButton(this IView obj, out Button value) => obj.TryGetValue(StartButton, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddStartButton(this IView obj, Button value) => obj.AddValue(StartButton, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelStartButton(this IView obj) => obj.DelValue(StartButton);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetStartButton(this IView obj, Button value) => obj.SetValue(StartButton, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasStartButton(this IView obj) => obj.HasValue(StartButton);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Button GetExitButton(this IView obj) => obj.GetValue<Button>(ExitButton);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetExitButton(this IView obj, out Button value) => obj.TryGetValue(ExitButton, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddExitButton(this IView obj, Button value) => obj.AddValue(ExitButton, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelExitButton(this IView obj) => obj.DelValue(ExitButton);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetExitButton(this IView obj, Button value) => obj.SetValue(ExitButton, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasExitButton(this IView obj) => obj.HasValue(ExitButton);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Button GetResumeButton(this IView obj) => obj.GetValue<Button>(ResumeButton);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetResumeButton(this IView obj, out Button value) => obj.TryGetValue(ResumeButton, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddResumeButton(this IView obj, Button value) => obj.AddValue(ResumeButton, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelResumeButton(this IView obj) => obj.DelValue(ResumeButton);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetResumeButton(this IView obj, Button value) => obj.SetValue(ResumeButton, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasResumeButton(this IView obj) => obj.HasValue(ResumeButton);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Button GetFinishButton(this IView obj) => obj.GetValue<Button>(FinishButton);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetFinishButton(this IView obj, out Button value) => obj.TryGetValue(FinishButton, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddFinishButton(this IView obj, Button value) => obj.AddValue(FinishButton, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelFinishButton(this IView obj) => obj.DelValue(FinishButton);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetFinishButton(this IView obj, Button value) => obj.SetValue(FinishButton, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasFinishButton(this IView obj) => obj.HasValue(FinishButton);
    }
}
