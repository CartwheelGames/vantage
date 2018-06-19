using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Guns;

namespace Units
{
    [DisallowMultipleComponent]
    public class UnitMobile : UnitBase
    {
        [SerializeField]
        private float speed = 1f;
		[SerializeField]
        private float rotSpeed = 1f;
        [SerializeField]
        private float scatterDistance = 25f;

        private UnitBase moveTarget = null;

        public void setMoveTarget(UnitBase newMoveTarget)
        {
            moveTarget = newMoveTarget;
        }

        protected override void Start()
        {
            base.Start();

            Vector3[] path = iTweenPath.GetPath("MovingUnitPath");

            Debug.Log(path);

            iTween.MoveTo(gameObject, iTween.Hash("path", path, "time", 5));
        }

		protected void Update()
        {
            if (!IsDead)
            {
                Vector3 myPos = transform.position;
                if (moveTarget != null)
                {
                    Vector3 targetPos = moveTarget.transform.position;
                    float distanceToTarget = Vector3.Distance(targetPos, myPos);
                    Vector3 direction;
                    if (distanceToTarget > scatterDistance)
                    {
                        direction = (targetPos - myPos).normalized;
                    }
                    else
                    {
                        direction = (myPos - targetPos).normalized;
                    }
					Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
					transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotSpeed);
					transform.Translate(transform.forward * Time.deltaTime * speed);
                    foreach (GunBase gun in guns)
                    {
                        if (gun.GetIsAimedAtTarget(moveTarget.gameObject))
                        {
                            gun.FireProjectile();
                        }
                    }
                }
            }
            else
            {
                transform.Translate(Vector3.down);
            }
        }
    }
}
