using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // config
    [SerializeField] AudioClip[] tracks;

    // components
    AudioSource audioSource;

    // tracks enum (MUST UPDATE THIS AS NEW TRACKS ARE ADDED)
    public enum TrackEnumerator: int
    {
        mainMenu = 0,
        combat = 1,
        gameOver = 2,
        reaper = 3
    }

    // Awake is called first
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();  // track audiosource
        SetUpSingleton();                           // don't destroy on load
    }

    // Start is called before the first frame update
    private void Start()
    {
        ChangeTrack((int) TrackEnumerator.mainMenu);
    }

    // called by classes that need to change the music
    public void ChangeTrack(int trackNumber)
    {
        audioSource.loop = true;
        audioSource.clip = tracks[trackNumber];
        audioSource.Play();
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

    // unique function because it must turn off looping
    public void PlayGameOverTrack()
    {
        ChangeTrack((int)TrackEnumerator.gameOver);
        audioSource.loop = false;
    }
}
