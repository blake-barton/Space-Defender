using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    // config variables
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnTimeRandomFactor = 0.3f;
    [SerializeField] int enemyCountInWave = 5;
    [SerializeField] float enemySpeed = 5f;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }
    public float GetSpawnTimeRandomFactor() { return spawnTimeRandomFactor; }
    public int GetEnemyCountInWave() { return enemyCountInWave; }
    public float GetEnemySpeed() { return enemySpeed; }
    
    public List<Transform> GetWaypoints() 
    {
        var waveWayPoints = new List<Transform>();

        foreach (Transform waypoint in pathPrefab.transform)
        {
            waveWayPoints.Add(waypoint);
        }

        return waveWayPoints; 
    }
}
