using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void OnCollisionEnter2D(Collision2D otherObj)
    {
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
