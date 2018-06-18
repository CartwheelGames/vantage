using System.Collections;
using System.Linq;
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
		private void Start()
		{
            UnitSpawner[] spawners = FindObjectsOfType<UnitSpawner>();
            UnitBase[] allUnits = FindObjectsOfType<UnitBase>();
            teams = new Team[teamData.Length];
            for (int i = 0; i < teamData.Length; i++)
            {
                teams[i] = new Team(teamData[i]);
                foreach (UnitBase unit in allUnits)
                {
                    if (unit.MyTeamData == teamData[i])
                    {
                        teams[i].AddUnit(unit);
                    }
                }
            }
            foreach (Team team in teams)
            {
                Team[] enemies = teams.Where(t => t != team) as Team[];
                team.SetEnemies(enemies);
                foreach (UnitSpawner spawner in spawners)
                {
                    if (spawner.MyTeamData == team.teamData)
                    {
                        spawner.SetupTeam(team);
                    }
                }
            }
		}
	}

    public class Team
    {
        public TeamData teamData { get; private set; }
        private Team[] enemyTeamData = null;
        private List<UnitBase> units = new List<UnitBase>();

        public Team(TeamData data)
        {
            teamData = data;
        }

        public void SetEnemies(Team[] enemies)
        {
            enemyTeamData = enemies;
        }

        public void AddUnit(UnitBase unit)
        {
            if (!units.Contains(unit))
            {
                units.Add(unit);
            }
        }
    }
}
