using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    [SerializeField] float speedFactor = 2f;
    [SerializeField] float secondsActivated = 15f;

    private void Start()
    {
        SetUpSingleton();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IncreasePlayerSpeed(collision);
    }

    private void IncreasePlayerSpeed(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();    // get the player object
        if (!player) { return; }                                        // return if the collision isn't a player

        // set the current player's powerup to time powerup
        player.AddCurrentPowerUp(tag);

        player.TriggerSpeedIncrease(speedFactor, secondsActivated);
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
