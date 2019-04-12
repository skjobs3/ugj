using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyPainController : MonoBehaviour
{
    public float damage;
    public float speed;
    public Vector3 direction;

    private GameObject self;
    private float deathTimer = 0.0f;
    private const float deatTime = 5.0f;

    // Start is called before the first frame update

    public void SetSelf(GameObject self)
    {
        this.self = self;
    }

    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Supply")
        {
            Destroy(gameObject);
            return;
        }
        else if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().Hit((int)damage);
            Destroy(gameObject);
            return;
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            //disabled for now cause destroys on hitting self object idk why
            //if (collision.gameObject != self)
            //{
            //    Destroy(gameObject);
            //    return;
            //}
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
