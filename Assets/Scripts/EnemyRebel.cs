using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyRebel : MonoBehaviour
{
    // config variables
    [Header("Rebel Config")]
    [SerializeField] int health = 100;
    [SerializeField] float shotCounter;                 // When this hits 0, the enemy shoots
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] int scorePerKill = 10;
    [SerializeField] float firingAngle = 0;

    [Header("Effects")]
    [SerializeField] GameObject enemyProjectile;
    [SerializeField] AudioClip fireAudio;
    [SerializeField] [Range(0, 1)] float fireAudioVolume;
    [SerializeField] AudioClip deathAudio;
    [SerializeField] [Range(0, 1)] float deathAudioVolume;
    [SerializeField] GameObject explosionFX;
    [SerializeField] float durationOfExplosion = 1f;

    [Header("PowerUps")]
    [SerializeField] GameObject[] powerUps;
    [SerializeField] int percentChanceToDropPowerUp = 20;

    // variables
    Vector3 projectileOffset = new Vector3(0, -1, 0);

    // cached references
    GameSession gameSession;
    Player player;
    SceneLoader sceneLoader;
    EnemySpawner enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        player = FindObjectOfType<Player>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;  // shot counter decreases by time between frames
        if (shotCounter <= 0f)
        {
            // spawn laser
            AudioSource.PlayClipAtPoint(fireAudio, Camera.main.transform.position, fireAudioVolume);                 // play firing sound
            Instantiate(enemyProjectile, transform.position + projectileOffset, Quaternion.Euler(0, 0, firingAngle));

            // reset shot counter
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleDamage(collision);
    }

    private void HandleDamage(Collision2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }

        // take damgage
        health -= damageDealer.GetDamage();

        // death
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        SpawnPowerUp();
        GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);          // play explosion particle effect
        Destroy(explosion, durationOfExplosion);                                                          // delete the particle effect after a second
        AudioSource.PlayClipAtPoint(deathAudio, Camera.main.transform.position, deathAudioVolume);        // play death sound effect
        gameSession.AddToScore(scorePerKill);

        if (GetComponent<ReaperBossStages>())
        {
            player.SetHealth(99);
            gameSession.ResetTime();
            enemySpawner.StopSpawning();
            sceneLoader.LoadVictoryScene();
        }
    }

    private void SpawnPowerUp()
    {
        // chance to actually spawn a powerup
        int dieRoll = UnityEngine.Random.Range(1, 101);
        
        if (dieRoll <= percentChanceToDropPowerUp)
        {
            // get a random powerup index
            int powerUpIndex = UnityEngine.Random.Range(0, powerUps.Length);

            // check that player doesn't already have that powerup activated
            if (!player.GetCurrentPowerUps().Contains(powerUps[powerUpIndex].tag))
            {
                // spawn the powerup
                Instantiate(powerUps[powerUpIndex], transform.position, Quaternion.Euler(0, 0, 0));
            }
        }
    }

    // setters
    public void SetShotCounter(float newTime)
    {
        shotCounter = newTime;
    }

    // getters
    public int GetHealth()
    {
        return health;
    }
}
