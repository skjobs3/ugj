using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnimies : MonoBehaviour
{
    public GameObject[] enimies;
    public float periodMin = 5;
    public float periodMax = 10;
    public float velosity = 4.0f;
    private float timer = 0;
    private float nextTimeToTrigger = 0;
    private int nextIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer == 0f)
        {
            nextTimeToTrigger = Random.Range(periodMin, periodMax);

        }
        timer += Time.deltaTime;
        if (timer >= nextTimeToTrigger)
        {
            timer = 0;
            var GO = Instantiate(enimies[Random.Range(0, enimies.Length)], this.transform.position, Quaternion.identity);
            var rigitBody = GO.GetComponent<Rigidbody2D>();
            rigitBody.velocity = new Vector2(-velosity, Random.Range(-0.15f * velosity, 0.15f * velosity));
        }
    }
}
