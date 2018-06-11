using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units;
using Teams;

namespace Projectiles
{
    [DisallowMultipleComponent]
    public abstract class ProjectileBase : MonoBehaviour
    {
        [SerializeField]
        private Team myTeam = null;
        public Team MyTeam { get { return myTeam; } }
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
        [SerializeField]
        private GameObject deathEffect = null;
        private bool hasHit = false;
        private bool hasSetup = false;

        public void Setup (Team team)
        {
            if (hasSetup)
            {
                myTeam = team;
                hasSetup = true;
            }
        }

        private void Update()
        {
            if (!hasHit)
            {
                transform.Translate(Time.deltaTime * speed * Vector3.forward);
                if (timeAlive > lifeTime)
                {
                    TimeOut();
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
                UnitBase hitUnit = target.gameObject.GetComponent<UnitBase>();
                if (hitUnit != null)
                {
                    hitUnit.TakeDamage(power);
                }
                SpawnDeathEffect();
				hasHit = true;
                SelfDestruct();
            }
		}

        protected void TimeOut()
        {
            if (!hasHit)
            {
                hasHit = true;
                SpawnDeathEffect();
                SelfDestruct();
            }
        }

        private void SpawnDeathEffect()
        {
            if (deathEffect != null)
            {
                GameObject fx = Instantiate(deathEffect) as GameObject;
                if (fx != null)
                {
                    fx.transform.position = transform.position;
                }
            }           
        }

        private void SelfDestruct()
        {
            Destroy(gameObject);
        }
	}
}
