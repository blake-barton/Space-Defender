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

    [Header("State")]
    [SerializeField] bool bossFight = false;

    // cached reference
    ScoreDisplay scoreDisplay;
    EnemySpawner enemySpawner;
    MusicPlayer musicPlayer;

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
            enemySpawner.SetLoopWaves(false);
            musicPlayer.StopMusic();
            Instantiate(bossEnemy);
            musicPlayer.ChangeTrack((int) MusicPlayer.TrackEnumerator.reaper);
            bossFight = true;
        }
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public int GetScore()
    {
        return score;
    }
}
