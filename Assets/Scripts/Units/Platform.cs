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
    	private Turret [] turrets = null;


    	public void PointTurretsAtPoint (Vector3 point)
    	{
    		foreach (Turret turret in turrets)
    		{
                Debug.Log(turret);
                turret.PointAt(point);
    		}
    	}

    	public void FireTurrets ()
    	{
    		foreach (Turret turret in turrets)
    		{
                turret.FireProjectile();
    		}
    	}
    }
}
