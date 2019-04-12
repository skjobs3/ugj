using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyPainController : MonoBehaviour
{
    public float damage;
    public float speed;
    public Vector3 direction;

    private float deathTimer = 0.0f;
    private const float deatTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
            return;
        }
        else if (collision.gameObject.tag == "Player")
        {
            //collision.gameObject.GetComponent<PlayerController>().HP -= damage;
            Destroy(gameObject);
            return;
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        deathTimer += Time.deltaTime;
        if (deathTimer > deatTime)
        {
            Destroy(gameObject);
            return;
        }

        direction.Normalize();

        Vector3 pos = gameObject.transform.position;
        pos += direction * speed * Time.deltaTime;
        gameObject.transform.position = pos;
    }
}
