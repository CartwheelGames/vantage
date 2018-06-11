using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Teams;

namespace Units
{
    [DisallowMultipleComponent]
    public abstract class UnitSpawner : MonoBehaviour
    {
        [SerializeField]
        private Team myTeam = null;
        public Team MyTeam { get { return myTeam; } }
    }
}
