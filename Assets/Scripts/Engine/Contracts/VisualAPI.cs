/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Objects;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using Sample;
using Atomic.Elements;

namespace GameEngine
{
    public static class VisualAPI
    {
        ///Keys
        public const int Animator = 3; // Animator
        public const int SpriteRenderer = 8; // SpriteRenderer


        ///Extensions
        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Animator GetAnimator(this IAtomicObject obj) => obj.GetValue<Animator>(Animator);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAnimator(this IAtomicObject obj, out Animator value) => obj.TryGetValue(Animator, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAnimator(this IAtomicObject obj, Animator value) => obj.AddValue(Animator, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAnimator(this IAtomicObject obj) => obj.DelValue(Animator);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAnimator(this IAtomicObject obj, Animator value) => obj.SetValue(Animator, value);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SpriteRenderer GetSpriteRenderer(this IAtomicObject obj) => obj.GetValue<SpriteRenderer>(SpriteRenderer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetSpriteRenderer(this IAtomicObject obj, out SpriteRenderer value) => obj.TryGetValue(SpriteRenderer, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddSpriteRenderer(this IAtomicObject obj, SpriteRenderer value) => obj.AddValue(SpriteRenderer, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelSpriteRenderer(this IAtomicObject obj) => obj.DelValue(SpriteRenderer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSpriteRenderer(this IAtomicObject obj, SpriteRenderer value) => obj.SetValue(SpriteRenderer, value);
    }
}
