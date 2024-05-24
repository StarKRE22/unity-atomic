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
        public static Transform GetTransform(this IObject obj) => obj.GetValue<Transform>(Transform);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetTransform(this IObject obj, out Transform result) => obj.TryGet(Transform, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddTransform(this IObject obj, Transform reference) => obj.Put(Transform, reference);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelTransform(this IObject obj) => obj.Del(Transform);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetTransform(this IObject obj, Transform reference) => obj.Set(Transform, reference);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rigidbody2D GetRigidbody2D(this IObject obj) => obj.GetValue<Rigidbody2D>(Rigidbody2D);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetRigidbody2D(this IObject obj, out Rigidbody2D result) => obj.TryGet(Rigidbody2D, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddRigidbody2D(this IObject obj, Rigidbody2D reference) => obj.Put(Rigidbody2D, reference);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelRigidbody2D(this IObject obj) => obj.Del(Rigidbody2D);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRigidbody2D(this IObject obj, Rigidbody2D reference) => obj.Set(Rigidbody2D, reference);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EffectHolder GetEffectHolder(this IObject obj) => obj.GetValue<EffectHolder>(EffectHolder);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEffectHolder(this IObject obj, out EffectHolder result) => obj.TryGet(EffectHolder, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddEffectHolder(this IObject obj, EffectHolder reference) => obj.Put(EffectHolder, reference);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelEffectHolder(this IObject obj) => obj.Del(EffectHolder);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetEffectHolder(this IObject obj, EffectHolder reference) => obj.Set(EffectHolder, reference);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicObservable GetCollectCoinEvent(this IObject obj) => obj.GetValue<IAtomicObservable>(CollectCoinEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCollectCoinEvent(this IObject obj, out IAtomicObservable result) => obj.TryGet(CollectCoinEvent, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddCollectCoinEvent(this IObject obj, IAtomicObservable reference) => obj.Put(CollectCoinEvent, reference);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelCollectCoinEvent(this IObject obj) => obj.Del(CollectCoinEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCollectCoinEvent(this IObject obj, IAtomicObservable reference) => obj.Set(CollectCoinEvent, reference);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicVariable<float> GetLookDirection(this IObject obj) => obj.GetValue<IAtomicVariable<float>>(LookDirection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetLookDirection(this IObject obj, out IAtomicVariable<float> result) => obj.TryGet(LookDirection, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddLookDirection(this IObject obj, IAtomicVariable<float> reference) => obj.Put(LookDirection, reference);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelLookDirection(this IObject obj) => obj.Del(LookDirection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLookDirection(this IObject obj, IAtomicVariable<float> reference) => obj.Set(LookDirection, reference);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JumpComponent GetJumpComponent(this IObject obj) => obj.GetValue<JumpComponent>(JumpComponent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetJumpComponent(this IObject obj, out JumpComponent result) => obj.TryGet(JumpComponent, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddJumpComponent(this IObject obj, JumpComponent reference) => obj.Put(JumpComponent, reference);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelJumpComponent(this IObject obj) => obj.Del(JumpComponent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetJumpComponent(this IObject obj, JumpComponent reference) => obj.Set(JumpComponent, reference);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicAction GetDeathAction(this IObject obj) => obj.GetValue<IAtomicAction>(DeathAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetDeathAction(this IObject obj, out IAtomicAction result) => obj.TryGet(DeathAction, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddDeathAction(this IObject obj, IAtomicAction reference) => obj.Put(DeathAction, reference);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelDeathAction(this IObject obj) => obj.Del(DeathAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetDeathAction(this IObject obj, IAtomicAction reference) => obj.Set(DeathAction, reference);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MoveComponent GetMoveComponent(this IObject obj) => obj.GetValue<MoveComponent>(MoveComponent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetMoveComponent(this IObject obj, out MoveComponent result) => obj.TryGet(MoveComponent, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddMoveComponent(this IObject obj, MoveComponent reference) => obj.Put(MoveComponent, reference);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelMoveComponent(this IObject obj) => obj.Del(MoveComponent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetMoveComponent(this IObject obj, MoveComponent reference) => obj.Set(MoveComponent, reference);

    }
}
