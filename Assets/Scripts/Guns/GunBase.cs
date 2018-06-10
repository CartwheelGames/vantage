using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Projectiles;
namespace Guns
{
    public abstract class GunBase : MonoBehaviour
    {
		[SerializeField]
        private float cooldownDuration = 90f;
        private float cooldownProgress = 0f;
        [SerializeField]
        private float minPitch = 90f;
		[SerializeField]
        private float maxPitch = -90f;
        [SerializeField]
        private GameObject projectilePrefab;
        [SerializeField]
        private Transform projectileOrigin = null;

        private void Start()
        {
            cooldownProgress = cooldownDuration;
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
			cooldownProgress = 0;

            PlayFiringAnimation();

            GameObject instance = Instantiate(projectilePrefab,
                                          projectileOrigin.position,
                                          projectileOrigin.rotation);
            Projectile projectile = instance.GetComponent<Projectile>();
            if (projectile)
            {
                projectile.Setup();
            }
        }

        private void PlayFiringAnimation()
        {
            // TODO firing animation
        }
    }
}
