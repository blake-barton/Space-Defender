using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    [SerializeField] int healthIncrease = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();    // get the player object
        if (!player) { return; }                                        // return if the collision isn't a player

        player.AddHealth(healthIncrease);
    }
}
