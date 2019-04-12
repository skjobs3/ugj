﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : MonoBehaviour
{
    private bool m_isTaken = false;
    public bool IsTaken
    {
        get
        {
            return m_isTaken;
        }

        set
        {
            m_isTaken = value;
        }
    }

    public SupplyZone.SupplyType Type;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsInsidePickupArea(Collider2D collider)
    {
        return collider.IsTouching(gameObject.GetComponent<CircleCollider2D>()) || 
            collider.IsTouching(gameObject.GetComponent<BoxCollider2D>());
    }
}
