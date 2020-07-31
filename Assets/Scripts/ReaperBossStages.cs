using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperBossStages : MonoBehaviour
{
    // config variables
    [SerializeField] float healthPercentageToBeginWaves = .5f;

    // state
    [SerializeField] bool stageChanged = false;

    // utility
    float healthAsFloat;
    float maxHealth;

    // cached references
    EnemyRebel reaperEnemyComponent;
    EnemySpawner enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        reaperEnemyComponent = GetComponent<EnemyRebel>();
        maxHealth = reaperEnemyComponent.GetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        healthAsFloat = reaperEnemyComponent.GetHealth();
        if ((healthAsFloat <= maxHealth * healthPercentageToBeginWaves) && !stageChanged)
        RestartEnemyWaves();
    }

    private void RestartEnemyWaves()
    {
        stageChanged = true;
        enemySpawner.RestartSpawning();
    }
}
