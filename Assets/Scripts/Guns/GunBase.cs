using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Projectiles;
using Teams;

namespace Guns
{
    public abstract class GunBase : MonoBehaviour
    {
        [SerializeField]
        private TeamData myTeamData = null;
        public TeamData MyTeamData { get { return myTeamData; } }
        [SerializeField]
        private float cooldownDuration = 1f;
        private float cooldownTime = 0f;
        [SerializeField]
        private float rotSpeed = 1f;
        [SerializeField]
        private float minPitch = 90f;
        [SerializeField]
        private float maxPitch = -90f;
        [SerializeField]
        private float range = 150f;
        [SerializeField]
        private Transform barrel = null;
        [SerializeField]
        private GameObject projectilePrefab = null;
        [SerializeField]
        private Transform projectileOrigin = null;
        private bool hasSetup = false;

        protected virtual void Start()
        {
        }

        private void Setup(TeamData team)
        {
            if (!hasSetup)
            {
                myTeamData = team;
                hasSetup = true;
            }
        }

        public void FireProjectile()
        {
            if (projectilePrefab == null || Time.time < cooldownTime)
            {
                return;
            }
            cooldownTime = Time.time + cooldownDuration;

            PlayFiringAnimation();

            GameObject instance = Instantiate(projectilePrefab,
                                          projectileOrigin.position,
                                          projectileOrigin.rotation);
            ProjectileBase projectile = instance.GetComponent<ProjectileBase>();
            if (projectile)
            {
                projectile.Setup(myTeamData);
            }
        }

        public void PointAt(Vector3 point)
        {
            if (barrel == null)
            {
                return;
            }
            // Vector3 direction = (point - transform.position).normalized;
            //Quaternion targetRot = Quaternion.LookRotation(direction, transform.up);
            //Quaternion startRot = barrel.rotation;
            //float amount = Time.deltaTime * rotSpeed;
            //Quaternion endRot = Quaternion.Slerp(startRot, targetRot, amount);
            //Vector3 endAngle = endRot.eulerAngles;
            //transform.rotation = endRot;
            //float pitch = endAngle.y;
            //float pivot = endAngle.x;
            //barrel.eulerAngles = new Vector3(pitch, 0, 0);
            //transform.eulerAngles = new Vector3(0, pivot, 0);
            Vector3 dir = (point - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Quaternion endRotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotSpeed);
            Vector3 rotation = lookRotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            barrel.rotation = Quaternion.Slerp(barrel.rotation, lookRotation, Time.deltaTime * rotSpeed);
        }

        public bool GetIsAimedAtTarget(GameObject target)
        {
            Ray ray = new Ray(projectileOrigin.position, projectileOrigin.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, range, 1 << target.layer))
            {
                return hit.collider.gameObject == target;
            }
            return false;
        }

        private void PlayFiringAnimation()
        {
            // TODO firing animation
        }
    }
}
