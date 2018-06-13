using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Teams;

namespace Units
{
    [DisallowMultipleComponent]
    public abstract class UnitSpawnPoint : MonoBehaviour
    {
        [SerializeField]
        private Team myTeam = null;
        public Team MyTeam { get { return myTeam; } }
        [SerializeField]
        private float radius = 10f;
        public float Radius { get { return radius; } }
    }
}
