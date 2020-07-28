using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePowerUp : MonoBehaviour
{
    [Header("PowerUp Config")]
    [SerializeField] float playerFireRateMultiplier = 3f;
    [SerializeField] float secondsActivated = 20f;

    [Header("Effects")]
    [SerializeField] AudioClip activateAudio;
    [SerializeField] [Range(0, 1)] float activateAudioVolume = .5f;

    private void Start()
    {
        SetUpSingleton();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();    // get the player object
        if (!player) { return; }                                        // return if the collision isn't a player

        // play activate audio
        AudioSource.PlayClipAtPoint(activateAudio, Camera.main.transform.position, activateAudioVolume);

        // set the current player's powerup to time powerup
        player.AddCurrentPowerUp(tag);

        player.TriggerFireRateIncrease(playerFireRateMultiplier, secondsActivated);
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
