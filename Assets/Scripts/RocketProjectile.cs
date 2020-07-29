using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : MonoBehaviour
{
    // config variables
    [SerializeField] GameObject explosionFX;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] float explosionSoundVolume = 0.5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(explosionFX, transform.position, transform.rotation);

        // play sound if collision is not a shredder
        if (!collision.gameObject.CompareTag("Shredder"))
        {
            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, explosionSoundVolume);
        }
    }
}
