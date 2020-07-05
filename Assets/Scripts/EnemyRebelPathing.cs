using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRebelPathing : MonoBehaviour
{
    // THIS IS THE PATHING SCRIPT FOR EVERY ENEMY, NOT JUST THE REBEL. I'M TO AFRAID TO RENAME IT.

    // utility variables
    WaveConfig waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = waveConfig.GetEnemySpeed();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIndex < waypoints.Count)
        {
            var targetPosition = waypoints[waypointIndex].position;                                                 // target the next waypoint
            var distanceThisFrame = speed * Time.deltaTime;                                                         // get normalized distance

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, distanceThisFrame);        // move to waypoint

            // check if the waypoint has been reached
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // setters
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
}
