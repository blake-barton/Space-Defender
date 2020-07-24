using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // config variables
    [Header("Player Config")]
    [SerializeField] int health = 3;
    [SerializeField] float xSpeed = 10f;
    [SerializeField] float ySpeed = 10f;

    [Header("Projectile Config")]
    [SerializeField] float fireRate = 10f;
    [SerializeField] GameObject playerLaser;
    [SerializeField] AudioClip fireAudio;

    [Header("DeathFX")]
    [SerializeField] AudioClip deathAudio;
    [SerializeField] GameObject explosionFX;
    [SerializeField] float durationOfExplosion = 1f;

    [Header("Health Bar")]
    [SerializeField] Image[] healthBlocks;

    // coroutines
    Coroutine firingCoroutine;

    // variables
    Vector3 projectileOffset = new Vector3(0, 1, 0);
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // cached references
    Camera gameCamera;
    BoxCollider2D playerCollider;
    SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        SetUpMoveBoundaries();
        sceneLoader = FindObjectOfType<SceneLoader>();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        for (int i = 0; i < healthBlocks.Length; i++)
        {
            if (i < health)
            {
                healthBlocks[i].enabled = true;
            }
            else
            {
                healthBlocks[i].enabled = false;
            }
        }
    }

    private void SetUpMoveBoundaries()
    {
        gameCamera = Camera.main;

        // find minimum values
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + playerCollider.bounds.size.x / 2f;     // ViewportToWorldPoint will find the actual coordinates of the game at the viewport's bottom left corner.
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + playerCollider.bounds.size.y / 2f;     // playerCollider is there to ensure player ship doesn't go halfway off screen

        // find maximum values
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - playerCollider.bounds.size.x / 2f;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - playerCollider.bounds.size.x / 2f;
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            // Do this so shots do not get layered on top of each other as the player hits fire
            StopCoroutine(firingCoroutine);
        }
    }

    // This is a Coroutine. It must have return type IEnumerator. It does its thing and then waits for the function inside to complete before doing it again.
    IEnumerator FireContinuously()
    {
        // wrapped in a while loop so that the ship keeps firing while the button is down
        while (true)
        {
            Instantiate(playerLaser, transform.position + projectileOffset, transform.rotation);
            AudioSource.PlayClipAtPoint(fireAudio, Camera.main.transform.position);

            yield return new WaitForSeconds(1 / fireRate);
        }

    }

    private void Move()
    {
        float xMovement = Input.GetAxis("Horizontal") * xSpeed;
        float yMovement = Input.GetAxis("Vertical") * ySpeed;

        // multiply by delta time so speed is not tied to frame rate.
        xMovement *= Time.deltaTime;
        yMovement *= Time.deltaTime;

        // move player
        transform.Translate(xMovement, yMovement, 0);

        // clamp movement to viewport
        Vector2 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, xMin, xMax);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, yMin, yMax);
        transform.position = clampedPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check that the object colliding has a DamageDealer component
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }

        // decrease health on hit
        health -= damageDealer.GetDamage();

        // Kill player when health reaches 0
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        healthBlocks[0].enabled = false;                                                                   // hide last health block
        AudioSource.PlayClipAtPoint(deathAudio, Camera.main.transform.position);                           // play death audio
        GameObject explosion = Instantiate(explosionFX, transform.position, transform.rotation);           // play explosion effect
        Destroy(explosion, durationOfExplosion);                                                           // delete the particle effect after a second
        sceneLoader.LoadGameOver();                                                                        // load death screen
        Destroy(gameObject);                                                                               // destroy player
    }
}
