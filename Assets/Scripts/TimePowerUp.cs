using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePowerUp : MonoBehaviour
{
    // config variables
    [SerializeField] float newTimeScale = 0.5f;
    [SerializeField] float playerSpeedMultiplier = 2f;
    [SerializeField] float playerFireRateMultiplier = 2f;
    [SerializeField] float secondsActivated = 5f;
    [SerializeField] float musicPitchMultiplier = .5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();    // get the player object
        if (!player) { return; }                                        // return if the collision isn't a player

        player.TriggerSlowTime(newTimeScale, playerSpeedMultiplier, playerFireRateMultiplier, musicPitchMultiplier, secondsActivated);
    }
}
