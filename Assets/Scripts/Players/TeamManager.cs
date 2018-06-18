using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units;
namespace Teams
{
    public class TeamManager : MonoBehaviour
    {
        [SerializeField]
        private TeamData[] teamData = new TeamData[0];
        [SerializeField]
        private Team[] teams = new Team[0];
        [SerializeField]
        private UnitSpawner[] spawners = null;

		private void Start()
		{
            UnitBase[] allUnits = FindObjectsOfType<UnitBase>();
            teams = new Team[teamData.Length];
            for (int i = 0; i < teamData.Length; i++)
            {
                teams[i] = new Team();
                foreach (UnitBase unit in allUnits)
                {
                    if (unit.MyTeam == teamData[i])
                    {
                        
                    }
                }
            }
		}
	}

    [System.Serializable]
    public class Team
    {
        private List<UnitBase> units = new List<UnitBase>();
        public void AddUnit(UnitBase unit)
        {
            
        }
    }
}
