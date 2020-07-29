using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : MonoBehaviour
{
    // config variables
    [SerializeField] GameObject explosionFX;
    [SerializeField] float durationOfExplosion = 1;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] [Range(0, 1)] float explosionSoundVolume = 0.5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // spawn/destroy particle effect
        GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);

        // play sound if collision is not a shredder
        if (!collision.gameObject.CompareTag("Shredder"))
        {
            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, explosionSoundVolume);
        }
    }
}
