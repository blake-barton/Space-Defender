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

    // Awake is called first
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();  // track audiosource
        SetUpSingleton();                           // don't destroy on load
    }

    // Start is called before the first frame update
    private void Start()
    {
        PickAudioClip();
        audioSource.Play();
    }

    private void PickAudioClip()
    {
        // TODO - select track based on scene
        //  MainMenu, Rules -> "Starship Troopers"
        //  GameScreen -> "Combat"
        //  DeathScreen -> "Death"
        audioSource.clip = tracks[0];
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
}
