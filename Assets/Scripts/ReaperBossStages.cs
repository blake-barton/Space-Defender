using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperBossStages : MonoBehaviour
{
    // config variables
    [SerializeField] float healthPercentageToBeginWaves = .5f;
    [SerializeField] float secondsToHoldFire = 4f;
    [SerializeField] Color colorFlashOnHit;
    [SerializeField] float colorFlashTime = .5f;

    // state
    [SerializeField] bool stageChanged = false;

    // utility
    float healthAsFloat;
    float maxHealth;
    Color originalColor;

    // cached references
    EnemyRebel reaperEnemyComponent;
    EnemySpawner enemySpawner;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        enemySpawner = FindObjectOfType<EnemySpawner>();
        reaperEnemyComponent = GetComponent<EnemyRebel>();
        maxHealth = reaperEnemyComponent.GetHealth();
        reaperEnemyComponent.SetShotCounter(secondsToHoldFire);
    }

    // Update is called once per frame
    void Update()
    {
        // restarting enemy waves
        healthAsFloat = reaperEnemyComponent.GetHealth();
        if ((healthAsFloat <= maxHealth * healthPercentageToBeginWaves) && !stageChanged)
        RestartEnemyWaves();
    }

    private void RestartEnemyWaves()
    {
        stageChanged = true;
        enemySpawner.RestartSpawning();
    }

    private IEnumerator FlashOnHit()
    {
        Debug.Log("Color changed");
        spriteRenderer.color = colorFlashOnHit;
        yield return new WaitForSeconds(colorFlashTime);
        
        Debug.Log("Returning to original color.");
        spriteRenderer.color = originalColor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(FlashOnHit());
    }
}
