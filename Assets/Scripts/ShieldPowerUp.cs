using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    [SerializeField] GameObject energyShield;
    [SerializeField] AudioClip activateAudio;
    [SerializeField] [Range(0, 1)] float activateAudioVolume = .5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();    // get the player object
        if (!player) { return; }                                        // return if the collision isn't a player

        // play activate audio
        AudioSource.PlayClipAtPoint(activateAudio, Camera.main.transform.position, activateAudioVolume);

        Instantiate(energyShield, player.transform.position, player.transform.rotation);
    }
}
