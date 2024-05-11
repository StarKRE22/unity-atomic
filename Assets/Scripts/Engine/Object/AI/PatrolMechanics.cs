using System.Collections.Generic;
using Atomic.Elements;
using UnityEngine;

namespace Sample
{
    public sealed class PatrolMechanics
    {
        private readonly IEnumerator<Vector3> patrolPoints;
        private readonly Transform transform;
        private readonly IAtomicSetter<float> moveDirection;

        public PatrolMechanics(IEnumerator<Vector3> patrolPoints, IAtomicSetter<float> moveDirection, Transform transform)
        {
            this.patrolPoints = patrolPoints;
            this.moveDirection = moveDirection;
            this.transform = transform;
        }

        public void Start()
        {
            this.patrolPoints.MoveNext();
        }

        public void FixedUpdate()
        {
            Vector3 myPos = this.transform.position;
            Vector3 tarPos = this.patrolPoints.Current;
            Vector3 distance = tarPos - myPos;

            if (distance.magnitude <= 0.5)
            {
                this.patrolPoints.MoveNext();
            }
            else
            {
                this.moveDirection.Value = distance.normalized.x;
            }
        }
    }
}