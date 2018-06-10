using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units.Enemies;

public class Player : MonoBehaviour 
{

	private Camera camera;

	private void Start () 
	{

		camera = gameObject.GetComponent<Camera>();
	}
	

	private void Update () 
	{

		GameObject enemy = GetEnemyHitting();

		Debug.Log(enemy);

		Enemy enemyScript = enemy.GetComponent<Enemy>();

		if (!enemyScript.IsDead())
		{
			enemyScript.TakeDamage(5);
		}

	}

	private GameObject GetEnemyHitting () 
	{
		RaycastHit hit;

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        
        Physics.Raycast(ray, out hit);

        if (hit.collider != null)
        {
        	GameObject hitObject = hit.collider.gameObject;

            if (IsEnemy(hitObject))
            {
            	return hitObject;
            }

        }

        return null;
	}

	private bool IsEnemy (GameObject target)
	{
		return target != null && target.GetComponent<Enemy>() != null;
	}
}
