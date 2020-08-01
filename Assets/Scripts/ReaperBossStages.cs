using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperBossStages : MonoBehaviour
{
    // config variables
    [Header("Staging")]
    [SerializeField] float healthPercentageToBeginWaves = .5f;
    [SerializeField] float secondsToHoldFire = 4f;
    [SerializeField] bool stageChanged = false;

    [Header("Hit Effects")]
    [SerializeField] Color colorFlashOnHit;
    [SerializeField] float colorFlashTime = .5f;
    [SerializeField] AudioClip[] hitAudioClips;
    [SerializeField] float hitAudioVolume = .5f;

    // utility
    float healthAsFloat;
    float maxHealth;
    Color originalColor;

    // cached references
    EnemyRebel reaperEnemyComponent;
    EnemySpawner enemySpawner;
    SpriteRenderer spriteRenderer;
    SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        enemySpawner = FindObjectOfType<EnemySpawner>();
        reaperEnemyComponent = GetComponent<EnemyRebel>();
        sceneLoader = FindObjectOfType<SceneLoader>();
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

        // checking if dead
        if (healthAsFloat <= 0)
        {
            Debug.Log("dead");
            sceneLoader.LoadVictoryScene();
        }
    }

    private void RestartEnemyWaves()
    {
        stageChanged = true;
        enemySpawner.RestartSpawning();
    }

    private IEnumerator FlashOnHit()
    {
        spriteRenderer.color = colorFlashOnHit;
        yield return new WaitForSeconds(colorFlashTime);
        
        spriteRenderer.color = originalColor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // play a random audio clip
        AudioSource.PlayClipAtPoint(hitAudioClips[Random.Range(0, hitAudioClips.Length)], Camera.main.transform.position, hitAudioVolume);

        // flash color
        StartCoroutine(FlashOnHit());
    }
}
