using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePowerUp : MonoBehaviour
{
    [SerializeField] float playerFireRateMultiplier = 3f;
    [SerializeField] float secondsActivated = 20f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();    // get the player object
        if (!player) { return; }                                        // return if the collision isn't a player

        player.MultiplyFireRate(playerFireRateMultiplier);
    }
}
