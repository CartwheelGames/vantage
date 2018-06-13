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
        private Team myTeam = null;
        public Team MyTeam { get { return myTeam; } }
        [SerializeField]
        private float cooldownDuration = 90f;
        private float cooldownProgress = 0f;
        [SerializeField]
        private float rotSpeed = 1f;
        [SerializeField]
        private float minPitch = 90f;
        [SerializeField]
        private float maxPitch = -90f;
        [SerializeField]
        private Transform barrel = null;
        [SerializeField]
        private GameObject projectilePrefab = null;
        [SerializeField]
        private Transform projectileOrigin = null;
        private bool hasSetup = false;

        private void Start()
        {
            cooldownProgress = cooldownDuration;
        }

        private void Setup(Team team)
        {
            if (!hasSetup)
            {
                myTeam = team;
                hasSetup = true;
            }
        }

        public void FireProjectile()
        {
            if (projectilePrefab == null)
            {
                return;
            }
            if (cooldownProgress < cooldownDuration)
            {
                cooldownProgress += Time.deltaTime;
                return;
            }
            cooldownProgress = 0f;

            PlayFiringAnimation();

            GameObject instance = Instantiate(projectilePrefab,
                                          projectileOrigin.position,
                                          projectileOrigin.rotation);
            ProjectileBase projectile = instance.GetComponent<ProjectileBase>();
            if (projectile)
            {
                projectile.Setup(myTeam);
            }
        }

        public void PointAt(Vector3 point)
        {
            if (barrel == null)
            {
                return;
            }
            Quaternion targetRot = Quaternion.LookRotation(point, transform.up);
            Quaternion startRot = barrel.rotation;
            float amount = Time.deltaTime * rotSpeed;
            Quaternion endRot = Quaternion.Slerp(startRot, targetRot, amount);
            Vector3 endAngle = endRot.eulerAngles;
            float pitch = Mathf.Clamp(endAngle.y, minPitch, maxPitch);
            barrel.eulerAngles = new Vector3(endAngle.x, pitch, endAngle.z);
        }

        private void PlayFiringAnimation()
        {
            // TODO firing animation
        }
    }
}
