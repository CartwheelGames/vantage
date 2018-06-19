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
                        unit.Setup(teams[i]);
                    }
                }
            }
            foreach (Team team in teams)
            {
                List<Team> enemies = new List<Team>();
                foreach (Team otherTeam in teams)
                {
                    if (otherTeam != team)
                    {
                        enemies.Add(team);
                    }
                }
                team.SetEnemies(enemies.ToArray());
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
        private Team[] enemyTeams = null;
        public List<UnitBase> units = new List<UnitBase>();

        public Team(TeamData data)
        {
            teamData = data;
        }

        public void SetEnemies(Team[] enemies)
        {
            enemyTeams = enemies;
        }

        public void AddUnit(UnitBase unit)
        {
            if (!units.Contains(unit))
            {
                units.Add(unit);
            }
        }

        public UnitBase GetNearestEnemyUnit(Vector3 point, float distance = float.MaxValue)
        {
            UnitBase nearestEnemy = null;
            foreach (UnitBase enemy in enemyTeams[0].units)
            {
                float currentDistance = Vector3.Distance(point, enemy.transform.position);
                if (currentDistance < distance)
                {
                    distance = currentDistance;
                    nearestEnemy = enemy;
                }
            }
            return nearestEnemy;
        }
    }
}
