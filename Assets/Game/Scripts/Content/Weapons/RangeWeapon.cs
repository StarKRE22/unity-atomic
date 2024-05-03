using Atomic;
using Game.Engine;
using UnityEngine;

namespace Game.Content
{
    public sealed class RangeWeapon : Weapon
    {
        public override IAtomicFunction<bool> FireCondition => this.fireCondiiton;
        public IAtomicAction FireAction => this.fireAction;
        public IAtomicValue<int> Charges => this.charges;
        
        [SerializeField]
        private Transform firePoint;

        [SerializeField]
        private AtomicAnd fireCondiiton;

        [SerializeField]
        private AtomicAction fireAction;

        [SerializeField]
        private GameObject bulletPrefab;

        [SerializeField]
        private SpawnObjectAction spawnObjectAction;

        [SerializeField]
        private Countdown countdown;

        [SerializeField]
        private AtomicVariable<int> charges;

        private void Awake()
        {
            this.spawnObjectAction.Compose(
                this.firePoint, this.bulletPrefab
            );
            
            this.fireCondiiton.Append(new AtomicFunction<bool>(() => this.charges.Value > 0));
            
            this.fireAction.Compose(() =>
            {
                if (this.countdown.IsEnded())
                {
                    this.spawnObjectAction.Invoke();
                    this.charges.Value--;
                    this.countdown.Reset();
                }
            });
        }

        public void FixedUpdate()
        {
            this.countdown.Tick(Time.fixedDeltaTime);
        }
    }
}