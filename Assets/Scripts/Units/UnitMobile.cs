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
        [SerializeField]
        private string pathName = "MovingUnitPath";
        [SerializeField]
        private int moveDuration = 100;

        private UnitBase target = null;
          

        protected override void Start()
        {
            base.Start();

            MoveAlongPath();
        }

        protected void Update()
        {
            
            if (!IsDead && MyTeam != null)
            {
                UpdateTarget();
                FireGunsAtTarget();
            }
        }

        private void UpdateTarget()
        {
            UnitBase newTarget = MyTeam.GetNearestEnemyUnit(transform.position);

            if (newTarget != null && newTarget != target)
            {
                target = newTarget;
            }   
        }

        private void FireGunsAtTarget()
        {

            if (target != null)
            {
                foreach (GunBase gun in guns)
                {
                    gun.PointAt(target.transform.position);

                    if (gun.GetIsAimedAtTarget(target.gameObject))
                    {
                        gun.FireProjectile();
                    }
                }
            }
        }

        private void MoveAlongPath()
        {
            Vector3[] path = iTweenPath.GetPath(pathName);
            iTween.MoveTo(gameObject, iTween.Hash("path", path, "time", moveDuration, "orientToPath", true, "looktime", 1));

        }
      
    }

}
