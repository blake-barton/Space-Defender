using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // config variables
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool loopWaves = false;
    [SerializeField] bool spawnRandomWaves = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSpawning());
    }

    IEnumerator StartSpawning()
    {
        if (spawnRandomWaves)
        {
            do
            {
                yield return StartCoroutine(SpawnWavesRandomly());
            }
            while (loopWaves);
        }
        else
        {
            do
            {
                yield return StartCoroutine(SpawnAllWaves());
            }
            while (loopWaves);
        }
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int i = startingWave; i < waveConfigs.Count; i++)
        {
            var currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnEnemiesInWave(WaveConfig waveConfig)
    {
        for (int i = 0; i < waveConfig.GetEnemyCountInWave(); i++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0]);
            newEnemy.GetComponent<EnemyRebelPathing>().SetWaveConfig(waveConfig);

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

    private IEnumerator SpawnWavesRandomly()
    {
        // get a random wave index
        int waveIndex = UnityEngine.Random.Range(0, waveConfigs.Count);
        var currentWave = waveConfigs[waveIndex];
        yield return StartCoroutine(SpawnEnemiesInWave(currentWave));
    }

    public void SetLoopWaves(bool isLooping)
    {
        loopWaves = isLooping;
    }

    public void RestartSpawning()
    {
        loopWaves = true;
        StartCoroutine(StartSpawning());
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
    }
}
