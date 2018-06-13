using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Units
{
    public class Platform : UnitStatic
    {
        [SerializeField]
        private Transform cameraPoint;
        public Vector3 TeleportPoint { get { return cameraPoint.position; } }
        [SerializeField]
        private Turret[] turrets = null;
        [SerializeField]
        private GameObject sensorColumn = null;
        public bool IsSensorColumnShown 
        {
            get
            {
                return sensorColumn != null && sensorColumn.activeSelf;
            }
            set
            {
                if (sensorColumn != null && sensorColumn.activeSelf != value)
                {
                    sensorColumn.SetActive(value);
                }
            }
        }

        public void PointTurretsAtPoint(Vector3 point)
        {
            foreach (Turret turret in turrets)
            {
                if (turret != null)
                {
                    turret.PointAt(point);
                }
            }
        }

        public void FireTurrets()
        {
            foreach (Turret turret in turrets)
            {
                if (turret != null)
                {
                    turret.FireProjectile();
                }
            }
        }
    }
}
