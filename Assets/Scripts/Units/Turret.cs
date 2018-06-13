using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Guns;

namespace Units
{
	public class Turret : UnitStatic 
	{
		[SerializeField]
		private GunBase gun;

		public void PointAt (Vector3 point) 
		{
			gun.PointAt(point);
		}

		public void FireProjectile ()
		{
			gun.FireProjectile();
		}
	

	}
}
