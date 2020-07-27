using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShield : MonoBehaviour
{
    // config variables
    [SerializeField] int hitsToDestroy = 3;

    // cached references
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        LockPositionToPlayer();
    }

    private void LockPositionToPlayer()
    {
        transform.position = player.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Energy shield hit: " + collision.gameObject.name + " | health = " + hitsToDestroy);
        hitsToDestroy--;

        if (hitsToDestroy <= 0)
        {
            Destroy(gameObject);
        }
    }
}
