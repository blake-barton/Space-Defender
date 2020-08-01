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
    [SerializeField] [Range(0, 1)] float deactivateAudioVolume = 0.5f;

    // cached references
    Player player;
    ShieldCountUI shieldCountUI;

    // Start is called before the first frame update
    void Start()
    {
        SetUpSingleton();
        player = FindObjectOfType<Player>();
        shieldCountUI = FindObjectOfType<ShieldCountUI>();
    }

    // Update is called once per frame
    void Update()
    {
        LockPositionToPlayer();
        shieldCountUI.UpdateShieldCountText(health);
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Energy shield hit: " + collision.gameObject.name + " | health = " + health);

        // ion pulse destroys shield instantly
        if (collision.gameObject.CompareTag("IonPulse"))
        {
            AudioSource.PlayClipAtPoint(deactivateAudio, Camera.main.transform.position, deactivateAudioVolume);    // play damage audio
            health = 0;
            Die();
        }
        else
        {
            health--;                                                                                               // decrement health
            AudioSource.PlayClipAtPoint(deactivateAudio, Camera.main.transform.position, deactivateAudioVolume);    // play damage audio

            if (health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        health = 0;                                     // so that continuous contact doesn't bring shield into negatives
        shieldCountUI.UpdateShieldCountText(health);    // need this to get '0' to display
        Destroy(gameObject);
    }
}
