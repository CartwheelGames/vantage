using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Units
{
    public class Unit : MonoBehaviour
    {
        private bool isDead = false;
        [SerializeField]
        private float speed = 1f;
        [SerializeField]
        private float armor = 0;
        private const float armorReduction = 0.1f;
        [SerializeField]
        private float healthPoints = 100f;
        public float HealthPoints
        {
            get
            {
                return healthPoints;
            }
        }

        protected void TakeDamage(float damage)
        {
            damage -= damage * (armor * armorReduction);
            if (damage > 0)
            {
                healthPoints -= damage;
                if (healthPoints <= 0 && !isDead)
                {
                    Die();
                }
            }
        }

        protected void Die()
        {
            // TODO Explosion Effects here
            // TODO recycle to an object pool, rather than destroy this gameObject.
            isDead = true;
            Invoke("Remove", 1f);
            Destroy(gameObject);
        }

        private void Remove()
        {
            Destroy(gameObject);
        }

        protected void Update()
        {
            if (!isDead)
            {
                transform.Translate(Vector3.forward * speed);
            }
            else
            {
                transform.Translate(Vector3.down);
            }
        }
    }
}
