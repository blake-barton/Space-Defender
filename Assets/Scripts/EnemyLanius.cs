using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLanius : MonoBehaviour
{
    [SerializeField] int health = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        ProcessHit(collision, damageDealer);
    }

    private void ProcessHit(Collision2D collision, DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        if (!damageDealer) { return; }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
