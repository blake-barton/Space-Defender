using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    // config
    [SerializeField] int score = 0;

    // cached reference
    ScoreDisplay scoreDisplay;

    private void Awake()
    {
        SetUpSingleton();
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreDisplay = FindObjectOfType<ScoreDisplay>();
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
