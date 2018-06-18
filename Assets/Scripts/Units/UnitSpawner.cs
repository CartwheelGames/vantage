using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Teams;

namespace Units
{
    [System.Serializable]
	struct Wave
	{
        [Range(1, 1000)]
        public int spawnCount;
        [Range(1f, 100f)]
        public float interval;
		[Range(0f, 10f)]
		public float timeOffset;
        public UnitSpawnPoint[] spawnPoints;
        public GameObject[] spawnPrefabs;
    }
    
    [DisallowMultipleComponent]
    public class UnitSpawner : MonoBehaviour
    {
        [SerializeField]
        private TeamData myTeam = null;
        public TeamData MyTeam { get { return myTeam; } }
        [SerializeField]
        private Wave[] waves = new Wave[0];
        private int waveIndex = 0;
        private int waveSpawnCount = 0;
        private float timeOfNextSpawn = 0f;
        private bool isDone = false;
		private void Update()
		{
            if (isDone)
            {
                return;
            }
            Wave wave = waves[waveIndex];
            if (waveSpawnCount > wave.spawnCount)
            {
                waveIndex++;
                if (waveIndex >= waves.Length)
                {
                    isDone = true;
                }
                else
                {
					waveSpawnCount = 0;
                    timeOfNextSpawn = DetermineTimeOfNextSpawn();
                }
            }
            else if (Time.time > timeOfNextSpawn)
            {
                UnitBase unit = SpawnUnit();
                if (unit != null)
                {
                    unit.Setup(myTeam);
                    waveSpawnCount++;
                }
                timeOfNextSpawn = DetermineTimeOfNextSpawn();
            }
		}

        private float DetermineTimeOfNextSpawn()
        {
            if (waveIndex >= waves.Length)
            {
                return 0f;
            }
            Wave wave = waves[waveIndex];
            float timeOffset = Random.Range(0, wave.timeOffset);
            return Time.time + wave.interval + timeOffset;
        }

        private UnitBase SpawnUnit()
        {
            if (waveIndex >= waves.Length)
            {
                return null;
            }
            Wave wave = waves[waveIndex];
            UnitSpawnPoint[] spawnPoints = wave.spawnPoints;
			int spawnIndex = Random.Range(0, spawnPoints.Length);
            UnitSpawnPoint spawnPoint = spawnPoints[spawnIndex];
            if (spawnPoint.MyTeam != myTeam)
            {
                Debug.LogWarning("Could not spawn unit for team "
                                 + myTeam.DisplayName
                                 + " at spawn point owned by "
                                 + spawnPoint.MyTeam.DisplayName);
                return null;
            }
            GameObject[] spawnPrefabs = wave.spawnPrefabs;
            Vector3 offset = Random.insideUnitSphere * spawnPoint.Radius;
            Vector3 position = spawnPoint.transform.position + offset;
            int prefabIndex = Random.Range(0, spawnPrefabs.Length);
            GameObject instance = Instantiate(spawnPrefabs[prefabIndex],
                                              position,
                                              spawnPoint.transform.rotation);
            if (instance != null)
            {
                UnitBase unit = instance.GetComponent<UnitBase>();
                if (unit != null)
                {
                    return unit;
                }
            }
            return null;
        }
	}
}
