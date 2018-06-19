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
                Team[] enemies = teams.Where(t => t != team).ToArray();
                Debug.Log(teams.Where(t => t != team));
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
        private Team[] enemyTeams = null;
        private List<UnitBase> units = new List<UnitBase>();
        public UnitBase[] Units {get{ return Units.ToArray();}}

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
        public UnitBase[] GetEnemyUnits()
        {
            if (enemyTeams == null || enemyTeams.Length == 0)
            {
                return new UnitBase[0];
            }
            return enemyTeams.SelectMany(team => team.Units).ToArray();
        }

        public UnitBase GetNearestEnemyUnit(Vector3 point, float distance = float.MaxValue)
        {
            UnitBase[] enemies = GetEnemyUnits();
            UnitBase nearestEnemy = null;
            if (enemies.Length > 0)
            {
                foreach (UnitBase enemy in enemies)
                {
                    float currentDistance = Vector3.Distance(point, enemy.transform.position);
                    if (currentDistance < distance)
                    {
                        distance = currentDistance;
                        nearestEnemy = enemy;
                    }
                }
            }
            return nearestEnemy;
        }
    }
}
