using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Guns;
using Teams;

namespace Units
{
    [DisallowMultipleComponent]
    public abstract class UnitBase : MonoBehaviour
    {
        [SerializeField]
        private Team myTeam = null;
        public Team MyTeam { get { return myTeam; } }
        public bool IsDead { get; private set; }
        [SerializeField]
        private float armor = 0;
        private const float armorReduction = 0.1f;
        [SerializeField]
        private float maxHealth = 100f;
        public float HealthPoints { get; private set; }
        [SerializeField]
        private Transform[] gunAttachPoints = new Transform[0];
        private GunBase[] guns = new GunBase[0];
        private bool hasSetup = false;

		private void Start()
        {
            HealthPoints = maxHealth;
		}

        public void Setup(GameObject[] gunPrefabs)
        {
            if (!hasSetup && gunPrefabs != null && gunPrefabs.Length > 0)
            {
                SetupGuns(gunPrefabs);
                hasSetup = true;
            }
        }

        public void SetupGuns(GameObject[] gunPrefabs)
        {
            guns = new GunBase[gunAttachPoints.Length];
            for (var i = 0; i < gunAttachPoints.Length; i++)
            {
                if (i >= gunPrefabs.Length
                    || gunPrefabs[i] == null
                    || gunAttachPoints[i] == null)
                {
                    continue;
                }
                GameObject instance = Instantiate(gunPrefabs[i],
                                                 gunAttachPoints[i]);
                if (instance)
                {
                    GunBase gun = instance.GetComponent<GunBase>();
                    if (gun)
                    {
                        guns[i] = gun;
                    }
                }
            }
        }

        public void HealDamage(float amount)
        {
            if (!IsDead)
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
                if (HealthPoints <= 0 && !IsDead)
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
            if (!IsDead)
            {
                HealthPoints = 0f;
                IsDead = true;
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
