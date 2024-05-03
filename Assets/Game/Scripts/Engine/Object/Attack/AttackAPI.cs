using Atomic;

namespace Game.Engine
{
    public static class AttackAPI
    {
        [Contract(typeof(IAtomicAction))]
        public const string AttackRequest = nameof(AttackRequest);
        
        [Contract(typeof(IAtomicValue<int>))]
        public const string WeaponCharges = nameof(WeaponCharges);
        
        [Contract(typeof(IAtomicAction))]
        public const string SwitchToMeleeWeapon = nameof(SwitchToMeleeWeapon);

        [Contract(typeof(IAtomicAction))]
        public const string SwitchToRangeWeapon = nameof(SwitchToRangeWeapon);
    }
}