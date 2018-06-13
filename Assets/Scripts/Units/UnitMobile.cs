using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Units
{
    [DisallowMultipleComponent]
    public class UnitMobile : UnitBase
    {
        [SerializeField]
        private float speed = 1f;
		[SerializeField]
        private float rotSpeed = 1f;

        private UnitBase moveTarget = null;

        public void setMoveTarget(UnitBase newMoveTarget)
        {
            moveTarget = newMoveTarget;
        }

		protected void Update()
        {
            if (!IsDead)
            {
                if (moveTarget != null)
                {
                    Vector3 direction = (moveTarget.transform.position - transform.position).normalized;
                    Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotSpeed);
                    transform.Translate(transform.forward * Time.deltaTime * speed);
                }
            }
            else
            {
                transform.Translate(Vector3.down);
            }
        }
    }
}
