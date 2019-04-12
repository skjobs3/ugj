using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyPainController : MonoBehaviour
{
    public float damage;
    public float speed;
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().HP -= 16;
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Vector2.Angle(Vector2.up, direction);
        transform.eulerAngles = new Vector3(0, 0, direction.x > transform.position.x ? angle + 180 : -angle + 180);

        direction.Normalize();

        Vector3 pos = gameObject.transform.position;
        pos += direction * speed * Time.deltaTime;
        gameObject.transform.position = pos;
    }
}
