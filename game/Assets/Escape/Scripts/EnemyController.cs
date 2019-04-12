using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float speed;

    void GotoZeroPoint()
    {
    }

    void SetLook(Vector3 targetPosition, Vector3 directionVector)
    {
        float angle = Vector2.Angle(Vector2.up, directionVector);
        transform.eulerAngles = new Vector3(0, 0, targetPosition.x > transform.position.x ? -angle + 90 : angle + 90);
    }

    void Move(Vector3 directionVector)
    {
        directionVector = directionVector.normalized * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + directionVector.x, transform.position.y + directionVector.y);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(0.0f, 0.0f, -1.0f);
        Vector3 directionVector = new Vector2(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y);
        SetLook(targetPosition, directionVector);
        if (GetComponent<Rigidbody2D>().velocity.magnitude < 0.5)
            Move(directionVector);
    }
}
