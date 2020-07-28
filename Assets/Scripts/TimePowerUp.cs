using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePowerUp : MonoBehaviour
{
    // config variables
    [Header("PowerUp Config")]
    [SerializeField] float newTimeScale = 0.5f;
    [SerializeField] float playerSpeedMultiplier = 2f;
    [SerializeField] float playerFireRateMultiplier = 2f;
    [SerializeField] float secondsActivated = 5f;
    [SerializeField] float musicPitchMultiplier = .5f;

    [Header("Effects")]
    [SerializeField] AudioClip activateAudio;
    [SerializeField] [Range(0, 1)] float activateAudioVolume = .5f;
    [SerializeField] AudioClip deactivateAudio;
    [SerializeField] [Range(0, 1)] float deactivateAudioVolume = .5f;

    private void Start()
    {
        SetUpSingleton();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();    // get the player object
        if (!player) { return; }                                        // return if the collision isn't a player

        // play activate sound
        AudioSource.PlayClipAtPoint(activateAudio, Camera.main.transform.position, activateAudioVolume);

        // set the current player's powerup to time powerup
        player.AddCurrentPowerUp(tag);

        // trigger the powerup
        player.TriggerSlowTime(newTimeScale, playerSpeedMultiplier, playerFireRateMultiplier, musicPitchMultiplier, secondsActivated, deactivateAudio, deactivateAudioVolume);
    }

    private void SetUpSingleton()
    {
        // if there's already a powerup on screen, destroy this one
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
