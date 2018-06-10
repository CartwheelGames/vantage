using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool isDead = false;
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
	private void TakeDamage (float damage)
    {
        damage -= damage * (armor * armorReduction);
        if (damage > 0)
        {
            healthPoints -= damage;
            if (healthPoints < 0 && !isDead)
            {
                Die();
            }
        }
	}

    private void Die()
    {
        // TODO Explosion Effects here
        // TODO recycle to an object pool, rather than destroy this gameObject.
        Destroy(gameObject);
    }

	private void Update ()
    {
        transform.Translate(Vector3.forward);
	}
}
