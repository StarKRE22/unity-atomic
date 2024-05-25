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
    public static class CommonAPI
    {
        ///Keys
        public const int Transform = 1; // Transform
        public const int Rigidbody2D = 2; // Rigidbody2D
        public const int EffectHolder = 7; // EffectHolder
        public const int CollectCoinEvent = 5; // IAtomicObservable
        public const int LookDirection = 14; // IAtomicVariable<float>
        public const int JumpComponent = 13; // JumpComponent
        public const int DeathAction = 23; // IAtomicAction
        public const int MoveComponent = 12; // MoveComponent
        public const int GameObject = 4; 


        ///Extensions
        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform GetTransform(this IAtomicObject obj) => obj.GetValue<Transform>(Transform);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetTransform(this IAtomicObject obj, out Transform result) => obj.TryGetValue(Transform, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddTransform(this IAtomicObject obj, Transform reference) => obj.AddValue(Transform, reference);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelTransform(this IAtomicObject obj) => obj.DelValue(Transform);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetTransform(this IAtomicObject obj, Transform reference) => obj.SetValue(Transform, reference);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rigidbody2D GetRigidbody2D(this IAtomicObject obj) => obj.GetValue<Rigidbody2D>(Rigidbody2D);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetRigidbody2D(this IAtomicObject obj, out Rigidbody2D result) => obj.TryGetValue(Rigidbody2D, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddRigidbody2D(this IAtomicObject obj, Rigidbody2D reference) => obj.AddValue(Rigidbody2D, reference);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelRigidbody2D(this IAtomicObject obj) => obj.DelValue(Rigidbody2D);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRigidbody2D(this IAtomicObject obj, Rigidbody2D reference) => obj.SetValue(Rigidbody2D, reference);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EffectHolder GetEffectHolder(this IAtomicObject obj) => obj.GetValue<EffectHolder>(EffectHolder);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEffectHolder(this IAtomicObject obj, out EffectHolder result) => obj.TryGetValue(EffectHolder, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddEffectHolder(this IAtomicObject obj, EffectHolder reference) => obj.AddValue(EffectHolder, reference);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelEffectHolder(this IAtomicObject obj) => obj.DelValue(EffectHolder);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetEffectHolder(this IAtomicObject obj, EffectHolder reference) => obj.SetValue(EffectHolder, reference);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicObservable GetCollectCoinEvent(this IAtomicObject obj) => obj.GetValue<IAtomicObservable>(CollectCoinEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCollectCoinEvent(this IAtomicObject obj, out IAtomicObservable result) => obj.TryGetValue(CollectCoinEvent, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddCollectCoinEvent(this IAtomicObject obj, IAtomicObservable reference) => obj.AddValue(CollectCoinEvent, reference);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelCollectCoinEvent(this IAtomicObject obj) => obj.DelValue(CollectCoinEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCollectCoinEvent(this IAtomicObject obj, IAtomicObservable reference) => obj.SetValue(CollectCoinEvent, reference);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicVariable<float> GetLookDirection(this IAtomicObject obj) => obj.GetValue<IAtomicVariable<float>>(LookDirection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetLookDirection(this IAtomicObject obj, out IAtomicVariable<float> result) => obj.TryGetValue(LookDirection, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddLookDirection(this IAtomicObject obj, IAtomicVariable<float> reference) => obj.AddValue(LookDirection, reference);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelLookDirection(this IAtomicObject obj) => obj.DelValue(LookDirection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLookDirection(this IAtomicObject obj, IAtomicVariable<float> reference) => obj.SetValue(LookDirection, reference);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JumpComponent GetJumpComponent(this IAtomicObject obj) => obj.GetValue<JumpComponent>(JumpComponent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetJumpComponent(this IAtomicObject obj, out JumpComponent result) => obj.TryGetValue(JumpComponent, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddJumpComponent(this IAtomicObject obj, JumpComponent reference) => obj.AddValue(JumpComponent, reference);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelJumpComponent(this IAtomicObject obj) => obj.DelValue(JumpComponent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetJumpComponent(this IAtomicObject obj, JumpComponent reference) => obj.SetValue(JumpComponent, reference);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicAction GetDeathAction(this IAtomicObject obj) => obj.GetValue<IAtomicAction>(DeathAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetDeathAction(this IAtomicObject obj, out IAtomicAction result) => obj.TryGetValue(DeathAction, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddDeathAction(this IAtomicObject obj, IAtomicAction reference) => obj.AddValue(DeathAction, reference);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelDeathAction(this IAtomicObject obj) => obj.DelValue(DeathAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetDeathAction(this IAtomicObject obj, IAtomicAction reference) => obj.SetValue(DeathAction, reference);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MoveComponent GetMoveComponent(this IAtomicObject obj) => obj.GetValue<MoveComponent>(MoveComponent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetMoveComponent(this IAtomicObject obj, out MoveComponent result) => obj.TryGetValue(MoveComponent, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddMoveComponent(this IAtomicObject obj, MoveComponent reference) => obj.AddValue(MoveComponent, reference);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelMoveComponent(this IAtomicObject obj) => obj.DelValue(MoveComponent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetMoveComponent(this IAtomicObject obj, MoveComponent reference) => obj.SetValue(MoveComponent, reference);
    }
}
