using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : MonoBehaviour
{
    // config variables
    [SerializeField] GameObject explosionFX;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(explosionFX, transform.position, transform.rotation);
    }
}
