using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Units
{
    public class Platform : UnitStatic
    {
    	[SerializeField]
    	private GameObject teleportPoint;
    	public GameObject TeleportPoint { get { return teleportPoint; } }
    	[SerializeField]
    	private GameObject [] turrets = null;


    	public void PointTurretsAtTarget (GameObject target)
    	{
    		Debug.Log(target);
    		//UnitBase hitUnit = objectHitting.GetComponent<UnitBase>();
            //hitUnit.TakeDamage(5);
    	}
    }
}
