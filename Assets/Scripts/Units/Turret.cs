using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Guns;

namespace Units
{
	public class Turret : UnitStatic 
	{
		public void PointAt (Vector3 point) 
		{
            foreach (GunBase gun in guns)
            {
                if (gun != null)
                {
                    gun.PointAt(point);
                }
            }
		}

		public void FireProjectile ()
		{
            foreach (GunBase gun in guns)
            {
                if (gun != null)
                {
                    gun.FireProjectile();
                }
            }
		}
	}
}
