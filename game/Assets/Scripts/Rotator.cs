using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float Speed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rot = gameObject.transform.rotation;
        rot *= Quaternion.Euler(Vector3.forward * Speed * Time.deltaTime);
        gameObject.transform.rotation = rot;
    }
}
