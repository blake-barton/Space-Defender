using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] int score = 0;
    [SerializeField] int scoreToBeginBoss = 1000;
    [SerializeField] GameObject bossEnemy;

    [Header("Boss Settings")]
    [SerializeField] bool bossFight = false;
    [SerializeField] float startingXPos = .5f;
    [SerializeField] float startingYPos = 1f;

    // utility
    float originalTimeScale;
    float startingMusicPitch;

    // cached reference
    ScoreDisplay scoreDisplay;
    EnemySpawner enemySpawner;
    MusicPlayer musicPlayer;
    Camera gameCamera;

    private void Awake()
    {
        SetUpSingleton();
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreDisplay = FindObjectOfType<ScoreDisplay>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        musicPlayer = FindObjectOfType<MusicPlayer>();
        gameCamera = Camera.main;
        originalTimeScale = Time.timeScale;
        startingMusicPitch = musicPlayer.GetPitch();
    }

    private void SetUpSingleton()
    {
        // if there's more than on MusicPlayer, destroy this one, else don't destroy when changing scenes
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddToScore(int scorePerKill)
    {
        score += scorePerKill;   // increase score
        scoreDisplay.UpdateTextDisplay();

        // begin boss fight if score high enough and not already in boss fight
        if (score >= scoreToBeginBoss && !bossFight)
        {
            StartBossFight();
        }
    }

    private void StartBossFight()
    {
        enemySpawner.StopSpawning();                                                       // stop waves from spawning
        musicPlayer.ChangeTrack((int)MusicPlayer.TrackEnumerator.reaper);                      // change to boss music

        float yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, startingYPos, 0)).y;        // reaper y coordinate
        float xMax = gameCamera.ViewportToWorldPoint(new Vector3(startingXPos, 0, 0)).x;
        Vector3 spawnPosition = new Vector3(xMax, yMax, 0);

        Instantiate(bossEnemy, spawnPosition, Quaternion.identity);                             // spawn reaper
        bossFight = true;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetTime()
    {
        Time.timeScale = originalTimeScale;
        musicPlayer.SetPitch(startingMusicPitch);
    }
}
