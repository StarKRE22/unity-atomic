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
        public const int GameObject = 4; // GameObject
        public const int dddd = 6;

        ///Extensions
        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Transform GetTransform(this IAtomicObject obj) => obj.GetValue<Transform>(Transform);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetTransform(this IAtomicObject obj, out Transform value) => obj.TryGetValue(Transform, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddTransform(this IAtomicObject obj, Transform value) => obj.AddValue(Transform, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelTransform(this IAtomicObject obj) => obj.DelValue(Transform);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetTransform(this IAtomicObject obj, Transform value) => obj.SetValue(Transform, value);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rigidbody2D GetRigidbody2D(this IAtomicObject obj) => obj.GetValue<Rigidbody2D>(Rigidbody2D);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetRigidbody2D(this IAtomicObject obj, out Rigidbody2D value) => obj.TryGetValue(Rigidbody2D, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddRigidbody2D(this IAtomicObject obj, Rigidbody2D value) => obj.AddValue(Rigidbody2D, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelRigidbody2D(this IAtomicObject obj) => obj.DelValue(Rigidbody2D);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRigidbody2D(this IAtomicObject obj, Rigidbody2D value) => obj.SetValue(Rigidbody2D, value);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EffectHolder GetEffectHolder(this IAtomicObject obj) => obj.GetValue<EffectHolder>(EffectHolder);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEffectHolder(this IAtomicObject obj, out EffectHolder value) => obj.TryGetValue(EffectHolder, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddEffectHolder(this IAtomicObject obj, EffectHolder value) => obj.AddValue(EffectHolder, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelEffectHolder(this IAtomicObject obj) => obj.DelValue(EffectHolder);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetEffectHolder(this IAtomicObject obj, EffectHolder value) => obj.SetValue(EffectHolder, value);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicObservable GetCollectCoinEvent(this IAtomicObject obj) => obj.GetValue<IAtomicObservable>(CollectCoinEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetCollectCoinEvent(this IAtomicObject obj, out IAtomicObservable value) => obj.TryGetValue(CollectCoinEvent, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddCollectCoinEvent(this IAtomicObject obj, IAtomicObservable value) => obj.AddValue(CollectCoinEvent, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelCollectCoinEvent(this IAtomicObject obj) => obj.DelValue(CollectCoinEvent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetCollectCoinEvent(this IAtomicObject obj, IAtomicObservable value) => obj.SetValue(CollectCoinEvent, value);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicVariable<float> GetLookDirection(this IAtomicObject obj) => obj.GetValue<IAtomicVariable<float>>(LookDirection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetLookDirection(this IAtomicObject obj, out IAtomicVariable<float> value) => obj.TryGetValue(LookDirection, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddLookDirection(this IAtomicObject obj, IAtomicVariable<float> value) => obj.AddValue(LookDirection, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelLookDirection(this IAtomicObject obj) => obj.DelValue(LookDirection);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLookDirection(this IAtomicObject obj, IAtomicVariable<float> value) => obj.SetValue(LookDirection, value);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static JumpComponent GetJumpComponent(this IAtomicObject obj) => obj.GetValue<JumpComponent>(JumpComponent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetJumpComponent(this IAtomicObject obj, out JumpComponent value) => obj.TryGetValue(JumpComponent, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddJumpComponent(this IAtomicObject obj, JumpComponent value) => obj.AddValue(JumpComponent, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelJumpComponent(this IAtomicObject obj) => obj.DelValue(JumpComponent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetJumpComponent(this IAtomicObject obj, JumpComponent value) => obj.SetValue(JumpComponent, value);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IAtomicAction GetDeathAction(this IAtomicObject obj) => obj.GetValue<IAtomicAction>(DeathAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetDeathAction(this IAtomicObject obj, out IAtomicAction value) => obj.TryGetValue(DeathAction, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddDeathAction(this IAtomicObject obj, IAtomicAction value) => obj.AddValue(DeathAction, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelDeathAction(this IAtomicObject obj) => obj.DelValue(DeathAction);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetDeathAction(this IAtomicObject obj, IAtomicAction value) => obj.SetValue(DeathAction, value);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MoveComponent GetMoveComponent(this IAtomicObject obj) => obj.GetValue<MoveComponent>(MoveComponent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetMoveComponent(this IAtomicObject obj, out MoveComponent value) => obj.TryGetValue(MoveComponent, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddMoveComponent(this IAtomicObject obj, MoveComponent value) => obj.AddElement(MoveComponent, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DelMoveComponent(this IAtomicObject obj) => obj.DelElement(MoveComponent);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetMoveComponent(this IAtomicObject obj, MoveComponent value) => obj.SetElement(MoveComponent, value);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GameObject GetGameObject(this IAtomicObject obj) => obj.GetValue<GameObject>(GameObject);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetGameObject(this IAtomicObject obj, out GameObject value) => obj.TryGetValue(GameObject, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddGameObject(this IAtomicObject obj, GameObject value) => obj.AddValue(GameObject, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelGameObject(this IAtomicObject obj) => obj.DelValue(GameObject);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetGameObject(this IAtomicObject obj, GameObject value) => obj.SetValue(GameObject, value);

        [CanBeNull, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object Getdddd(this IAtomicObject obj) => obj.GetValue(dddd);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetdddd(this IAtomicObject obj, out object value) => obj.TryGetValue(dddd, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Adddddd(this IAtomicObject obj, object value) => obj.AddValue(dddd, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Deldddd(this IAtomicObject obj) => obj.DelValue(dddd);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Setdddd(this IAtomicObject obj, object value) => obj.SetValue(dddd, value);
    }
}
