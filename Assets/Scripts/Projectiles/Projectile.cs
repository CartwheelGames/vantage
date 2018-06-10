using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units;
namespace Projectiles
{
    [DisallowMultipleComponent]
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private float power = 1f;
        public float Power { get { return power; }}
        [SerializeField]
        private float lifeTime = 1f;
        public float LifeTime { get { return lifeTime; } }
        private float timeAlive = 0f;
        [SerializeField]
        private float speed = 1f;
        public float Speed { get { return speed; }}
        private bool hasHit = false;
        private void Update()
        {
            if (!hasHit)
            {
                transform.Translate(Time.deltaTime * speed * Vector3.forward);
                if (timeAlive > lifeTime)
                {
                    SelfDestruct();
                }
                else
                {
                    timeAlive += Time.deltaTime;
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!hasHit)
            {
                HitTarget(collision.gameObject);
            }
        }

        protected void HitTarget(GameObject target)
		{
            if (!hasHit)
            {
                Unit hitUnit = target.gameObject.GetComponent<Unit>();
                if (hitUnit != null)
                {
                    hitUnit.TakeDamage(power);
                }
				hasHit = true;
                Remove();
            }
		}

        protected void SelfDestruct()
        {
            if (!hasHit)
            {
                hasHit = true;
                Remove();
            }
        }

        private void Remove()
        {
            Destroy(gameObject);
        }
	}
}
