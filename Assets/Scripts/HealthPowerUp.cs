using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    [SerializeField] int healthIncrease = 1;
    [SerializeField] AudioClip activateAudio;
    [SerializeField] [Range(0, 1)] float activateAudioVolume = .5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();    // get the player object
        if (!player) { return; }                                        // return if the collision isn't a player

        // play activate audio
        AudioSource.PlayClipAtPoint(activateAudio, Camera.main.transform.position, activateAudioVolume);

        player.AddHealth(healthIncrease);
    }
}
