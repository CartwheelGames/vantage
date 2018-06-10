using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units.Enemies;

public class Player : MonoBehaviour 
{

	public Camera camera;

	private void Start () 
	{

		
	}
	

	private void Update () 
	{

		Enemy enemy = GetEnemyHitting();

		Debug.Log(enemy);

	}

	private Enemy GetEnemyHitting () 
	{
		RaycastHit hit;

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        
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
