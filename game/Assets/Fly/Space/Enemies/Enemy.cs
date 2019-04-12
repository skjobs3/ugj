using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fly.Ship.Modules.Projectile;

public class Enemy : MonoBehaviour
{
    public float Power = 50f;
    public int Health = 500;
    public int Damage = 100;
    public GameObject[] garbageObjects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        Debug.Log("Triggered");
        var projectile = otherObj.gameObject.GetComponent<Projectile>();
        if (projectile)
        {
            Health -= projectile.GetDamage();

            if (Health <= 0)
            {
                foreach (GameObject garbObjType in garbageObjects)
                {
                    var GO = Instantiate(garbObjType, this.transform.position, Quaternion.identity);
                    var velocity = UnityEngine.Random.insideUnitCircle * Power;
                    var rigitBody = GO.GetComponent<Rigidbody2D>();
                    rigitBody.velocity = velocity;
                }

                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D otherObj)
    {
        Debug.Log("Collide");
        var ship = otherObj.gameObject.GetComponentInParent<Fly.Ship.Ship>();
        if (ship)
        {
            Debug.Log("Ship");
            ship.Damage(Damage);
        }
    }
}
