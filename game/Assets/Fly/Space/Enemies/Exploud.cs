using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fly.Ship.Modules.Projectile;

public class Exploud : MonoBehaviour
{
    public float Power = 50f;
    public float Health = 500f;
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
        if (!projectile)
        {
            return;
        }

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
