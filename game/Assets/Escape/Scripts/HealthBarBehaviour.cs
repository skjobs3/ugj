using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarBehaviour : MonoBehaviour
{
    private float fullScaleX;

    private float totalHealth;
    private float leftHealth;

    public void SetTotal(float total)
    {
        totalHealth = total;
    }
    public void SetLeft(float left)
    {
        leftHealth = left;
    }

    // Start is called before the first frame update
    void Start()
    {
        fullScaleX = gameObject.transform.Find("Full").transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        float ratio = leftHealth / totalHealth;
        var scale = gameObject.transform.Find("Left").transform.localScale;
        scale.x = fullScaleX * ratio;
        gameObject.transform.Find("Left").transform.localScale = scale;
    }
}
