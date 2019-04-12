using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public float attackDistance = 2.1f;
    public float speed;

    private bool hasTarget = false;
    private Vector3 targetPosition = new Vector3();
    private float targetDistance = float.MaxValue;

    private float iWillHitObstacleSpeedFactor = 1.0f;
    private const float maxAffraidOfObstacleDistance = 10.0f;

    void updateTarget()
    {
        Vector3 targetPosition1 = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        Vector3 targetPosition2 = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

        if (player1 != null)
        {
            hasTarget = true;
            targetPosition1 = player1.transform.position;
        }

        if (player2 != null)
        {
            hasTarget = true;
            targetPosition2 = player2.transform.position;
        }

        if (hasTarget)
        {
            var player1Distance = Vector3.Distance(transform.position, targetPosition1);
            var player2Distance = Vector3.Distance(transform.position, targetPosition2);

            if (player2Distance < player1Distance)
            {
                targetPosition = targetPosition2;
                targetDistance = player2Distance;
            }
            else
            {
                targetPosition = targetPosition1;
                targetDistance = player1Distance;
            }
        }
    }

    void updateTransform()
    {
        if (hasTarget)
        {
            Vector3 directionVector = new Vector2(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y);
            SetLook(targetPosition, directionVector);
            if (GetComponent<Rigidbody2D>().velocity.magnitude < 0.5)
                Move(directionVector);
        }
    }

    void SetLook(Vector3 targetPosition, Vector3 directionVector)
    {
        float angle = Vector2.Angle(Vector2.up, directionVector);
        transform.eulerAngles = new Vector3(0, 0, targetPosition.x > transform.position.x ? -angle + 90 : angle + 90);
    }

    void Move(Vector3 directionVector)
    {
        directionVector = directionVector.normalized * speed * Time.deltaTime * iWillHitObstacleSpeedFactor;
        transform.position = new Vector3(transform.position.x + directionVector.x, transform.position.y + directionVector.y);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
        bool obstacleOnTheWay = false;
        Vector3 directionVector = new Vector3(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y, transform.position.z);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, directionVector);
        foreach(var hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject.tag == "Obstacle")
            {
                obstacleOnTheWay = true;
                var distance = Vector3.Distance(hit.collider.transform.position, transform.position);
                if (distance < maxAffraidOfObstacleDistance)
                {
                    iWillHitObstacleSpeedFactor = distance / maxAffraidOfObstacleDistance;
                }
            }
        }

        if(!obstacleOnTheWay)
        {
            iWillHitObstacleSpeedFactor = 1.0f;
        }
    }


    // Update is called once per frame
    void Update()
    {
        updateTarget();
        updateTransform();
    }
}
