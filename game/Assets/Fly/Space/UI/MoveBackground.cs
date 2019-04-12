using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        var renderer = gameObject.GetComponent<Renderer>();
        if (!renderer)
        {

            return;
        }

        var material = renderer.materials[0];
        if (!material)
        {
            return;
        }

        material.SetTextureOffset("_MainTex", material.GetTextureOffset("_MainTex") + new Vector2(0.01f * speed, 0));
    }
}
