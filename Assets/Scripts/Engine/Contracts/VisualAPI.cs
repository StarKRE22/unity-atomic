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
        public static Animator GetAnimator(this IAtomicObject obj) => obj.GetReference<Animator>(Animator);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetAnimator(this IAtomicObject obj, out Animator result) => obj.TryGetReference(Animator, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAnimator(this IAtomicObject obj, Animator reference) => obj.AddReference(Animator, reference);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelAnimator(this IAtomicObject obj) => obj.DelReference(Animator);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAnimator(this IAtomicObject obj, Animator reference) => obj.SetReference(Animator, reference);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SpriteRenderer GetSpriteRenderer(this IAtomicObject obj) => obj.GetReference<SpriteRenderer>(SpriteRenderer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetSpriteRenderer(this IAtomicObject obj, out SpriteRenderer result) => obj.TryGetReference(SpriteRenderer, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddSpriteRenderer(this IAtomicObject obj, SpriteRenderer reference) => obj.AddReference(SpriteRenderer, reference);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelSpriteRenderer(this IAtomicObject obj) => obj.DelReference(SpriteRenderer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSpriteRenderer(this IAtomicObject obj, SpriteRenderer reference) => obj.SetReference(SpriteRenderer, reference);
    }
}
