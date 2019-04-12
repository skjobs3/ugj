using System.Collections;
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
    }

    private SupplyZone.SupplyType m_type;
    public SupplyZone.SupplyType Type
    {
        get
        {
            return m_type;
        }
    }

    public GameObject AmmoOutfit;
    public GameObject FuelOutfit;

    private GameObject m_outfit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetType(SupplyZone.SupplyType type)
    {
        m_type = type;

        switch (m_type)
        {
            case SupplyZone.SupplyType.Ammo:
                m_outfit = Instantiate(AmmoOutfit, gameObject.transform);
                break;

            case SupplyZone.SupplyType.Fuel:
                m_outfit = Instantiate(FuelOutfit, gameObject.transform);
                break;

            default:
                Debug.Log("Unknown supply type");
                break;
        }
    }

    public bool IsInsidePickupArea(Collider2D collider)
    {
        return collider.IsTouching(m_outfit.GetComponent<CircleCollider2D>()) || 
            collider.IsTouching(m_outfit.GetComponent<BoxCollider2D>());
    }

    public void Take(Vector3 localPos, Transform socket)
    {
        gameObject.transform.SetParent(socket);
        gameObject.transform.localPosition = localPos;
        
        m_isTaken = true;
    }

    public void Drop(Vector3 dir)
    {
        gameObject.transform.SetParent(null);
        m_outfit.GetComponent<Rigidbody2D>().AddForce(dir);
        m_isTaken = false;
    }
}
