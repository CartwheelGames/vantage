﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Units.Enemies
{
    public class EnemySpawnPoint : UnitSpawnPoint
    {
        [SerializeField]
        private float radius = 0f;
        public float Radius
        {
            get
            {
                return Radius;
            }
        }
    }
}
