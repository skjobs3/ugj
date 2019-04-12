using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploud : MonoBehaviour
{
    public GameObject[] garbageObjects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision otherObj)
    {
        Debug.Log("AAAAAAAAA");
        Random rand = new Random();
        foreach (GameObject garbObj in garbageObjects)
        {
            Instantiate(garbObj, this.transform);
            var force = UnityEngine.Random.insideUnitCircle * 5;
            var rigitBody = garbObj.GetComponent<Rigidbody2D>();
            rigitBody.AddForce(force, ForceMode2D.Impulse);
        }

        Destroy(gameObject);
    }
}
