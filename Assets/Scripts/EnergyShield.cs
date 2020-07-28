using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShield : MonoBehaviour
{
    // config variables
    [Header("Config Values")]
    [SerializeField] int health = 3;
    [SerializeField] int maxHealth = 3;

    [Header("Effects")]
    [SerializeField] AudioClip deactivateAudio;
    [SerializeField] float deactivateAudioVolume = 0.5f;

    // cached references
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        SetUpSingleton();
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

    private void SetUpSingleton()
    {
        // if there's already a shield, destroy this one
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            FindObjectOfType<EnergyShield>().IncreaseHealth(maxHealth);
        }
    }

    public void IncreaseHealth(int healthIncrease)
    {
        health += healthIncrease;
        Debug.Log("Health increased");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Energy shield hit: " + collision.gameObject.name + " | health = " + health);
        health--;

        AudioSource.PlayClipAtPoint(deactivateAudio, Camera.main.transform.position, deactivateAudioVolume);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
