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
        public static Animator GetAnimator(this IObject obj) => obj.GetValue<Animator>(Animator);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAnimator(this IObject obj, out Animator result) => obj.TryGet(Animator, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAnimator(this IObject obj, Animator reference) => obj.Put(Animator, reference);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAnimator(this IObject obj) => obj.Del(Animator);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAnimator(this IObject obj, Animator reference) => obj.Set(Animator, reference);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SpriteRenderer GetSpriteRenderer(this IObject obj) => obj.GetValue<SpriteRenderer>(SpriteRenderer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetSpriteRenderer(this IObject obj, out SpriteRenderer result) => obj.TryGet(SpriteRenderer, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddSpriteRenderer(this IObject obj, SpriteRenderer reference) => obj.Put(SpriteRenderer, reference);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelSpriteRenderer(this IObject obj) => obj.Del(SpriteRenderer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSpriteRenderer(this IObject obj, SpriteRenderer reference) => obj.Set(SpriteRenderer, reference);
    }
}
