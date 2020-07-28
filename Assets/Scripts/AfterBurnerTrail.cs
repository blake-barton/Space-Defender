using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterBurnerTrail : MonoBehaviour
{
    // config variables
    [SerializeField] Vector2 playerOffset = new Vector2();

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
        LockToPlayer();
    }

    private void LockToPlayer()
    {
        // lock position to player if player exists
        if (player)
        {
            transform.position = (Vector2)player.transform.position - playerOffset;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
