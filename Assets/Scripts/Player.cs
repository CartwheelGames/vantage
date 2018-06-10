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
		GameObject objectHitting = GetObjectHitting();

		if (IsEnemy(objectHitting))
		{
			Enemy enemy = objectHitting.GetComponent<Enemy>();
			
			enemy.TakeDamage(5);

            Debug.Log(enemy);
		}
		else if (IsPlatform(objectHitting))
		{
			Debug.Log("PLATFORM");
		}

	}

	private GameObject GetObjectHitting ()
	{
		RaycastHit hit;

        Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
        
        Physics.Raycast(ray, out hit);

        if (hit.collider != null)
        {

        	return hit.collider.gameObject;
        }

        return null;
	}


	private bool IsEnemy (GameObject target)
	{
		return target != null && target.GetComponent<Enemy>() != null;
	}

	private bool IsPlatform (GameObject target)
	{
		return target != null && target.GetComponent<Platform>() != null;
	}
}
