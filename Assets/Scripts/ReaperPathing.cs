using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperPathing : MonoBehaviour
{
    // config variables
    [SerializeField] float speed = 10;

    // variables
    float xMin;
    float yMin;
    float xMax;
    float yMax;
    float nextXPos;
    float nextYPos;
    Vector2 nextWayPoint;

    // cached references
    Camera gameCamera;

    // Start is called before the first frame update
    void Start()
    {
        gameCamera = Camera.main;
        SetUpMovementBoundaries();
        GenerateNextWayPoint();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float movementThisFrame = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, nextWayPoint, movementThisFrame);

        if ((Vector2)transform.position == nextWayPoint)
        {
            GenerateNextWayPoint();
        }
    }

    private void GenerateNextWayPoint()
    {
        nextXPos = Random.Range(xMin, xMax);
        nextYPos = Random.Range(yMin, yMax);

        nextWayPoint = new Vector2(nextXPos, nextYPos);
    }

    private void SetUpMovementBoundaries()
    {
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }
}
