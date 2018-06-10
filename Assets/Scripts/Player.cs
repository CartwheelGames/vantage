using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units.Enemies;

public class Player : MonoBehaviour 
{

	private Camera myCamera;

	private void Start () 
	{

		myCamera = gameObject.GetComponent<Camera>();
	}
	

	private void Update () 
	{

		Enemy enemy = GetEnemyHitting();


		if (enemy != null && !enemy.IsDead)
		{
			enemy.TakeDamage(5);

            Debug.Log(enemy);
		}

	}

	private Enemy GetEnemyHitting () 
	{
		RaycastHit hit;

        Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
        
        Physics.Raycast(ray, out hit);

        if (hit.collider != null)
        {
        	GameObject hitObject = hit.collider.gameObject;

            if (IsEnemy(hitObject))
            {
            	return hitObject.GetComponent<Enemy>();
            }

        }

        return null;
	}

	private bool IsEnemy (GameObject target)
	{
		return target != null && target.GetComponent<Enemy>() != null;
	}
}
