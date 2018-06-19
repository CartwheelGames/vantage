using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Teams;

namespace Units
{
    [DisallowMultipleComponent]
    public class UnitSpawnPoint : MonoBehaviour
    {
        [SerializeField]
        private TeamData myTeamData = null;
        public TeamData MyTeamData { get { return myTeamData; } }
        [SerializeField]
        private float radius = 10f;
        public float Radius { get { return radius; } }
        [SerializeField]
        private string pathName = "";
        public string PathName { get { return pathName; }}
    }
}
