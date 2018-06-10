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
        private Team owner = null;
        public Team Owner { get { return owner; } }
    }
}