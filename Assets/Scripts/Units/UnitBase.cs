using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Teams;

namespace Units
{
    [DisallowMultipleComponent]
    public abstract class UnitBase : MonoBehaviour
    {
        [SerializeField]
        private Team owner = null;
        public Team Owner { get { return owner; } }
        private bool isDead = false;
        public bool IsDead { get { return isDead; } }
        [SerializeField]
        private float armor = 0;
        private const float armorReduction = 0.1f;
        [SerializeField]
        private float maxHealth = 100f;
        [SerializeField]
        private Transform[] gunAttachPoints = new Transform[0];
        public float HealthPoints { get; private set; }

		private void Start()
		{
            HealthPoints = maxHealth;
		}

        public void HealDamage(float amount)
        {
            if (!isDead)
            {
                HealthPoints = Mathf.Min(maxHealth, HealthPoints + amount);
            }
        }

        public void HealMax ()
        {
            HealDamage(maxHealth);
        }

        public void TakeDamage(float amount)
        {
            amount -= amount * (armor * armorReduction);
            if (amount > 0)
            {
                HealthPoints -= amount;
                if (HealthPoints <= 0 && !isDead)
                {
                    Die();
                }
            }
        }

        public void TakeMaxDamage()
        {
            TakeDamage(maxHealth);
        }

        private void Die()
        {
            // TODO Explosion Effects here
            // TODO recycle to an object pool, rather than destroy this gameObject.
            if (!isDead)
            {
                HealthPoints = 0f;
                isDead = true;
                Invoke("Remove", 1f);
                Destroy(gameObject);
            }
        }

        protected virtual void Remove()
        {
            Destroy(gameObject);
        }
    }
}
