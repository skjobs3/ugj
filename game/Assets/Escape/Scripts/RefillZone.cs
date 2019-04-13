using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillZone : MonoBehaviour
{
    public SupplyZone.SupplyType Type;

    public GameObject AmmoOutfitPrefab;
    public GameObject FuelOutfitPrefab;

    private GameObject m_activeOutfit;
    private List<Supply> m_activeSupplies = new List<Supply>();

    // Start is called before the first frame update
    void Start()
    {
        switch (Type)
        {
            case SupplyZone.SupplyType.Ammo:
                m_activeOutfit = Instantiate(AmmoOutfitPrefab, gameObject.transform);
                break;

            case SupplyZone.SupplyType.Fuel:
                m_activeOutfit = Instantiate(FuelOutfitPrefab, gameObject.transform);
                break;

            default:
                Debug.Assert(false, "Unknown supply type");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        List<Supply> leftSupplies = new List<Supply>();
        List<Supply> suppliesToRemove = new List<Supply>();

        foreach(Supply supply in m_activeSupplies)
        {
            if(supply.Type == Type && !supply.IsTaken)
            {
                suppliesToRemove.Add(supply);
            }
            else
            {
                leftSupplies.Add(supply);
            }
        }

        m_activeSupplies = leftSupplies;

        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        GameManager manager = cam.GetComponent<GameManager>();

        foreach(Supply supply in suppliesToRemove)
        {
            switch (supply.Type)
            {
                case SupplyZone.SupplyType.Ammo:
                    manager.CollectAmmo();
                    break;

                case SupplyZone.SupplyType.Fuel:
                    manager.CollectFuel();
                    break;

                default:
                    Debug.Assert(false, "Unknown supply type");
                    break;
            }

            Destroy(supply.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag != "Supply")
        {
            return;
        }

        Supply supply = collider.gameObject.GetComponent<Supply>();
        m_activeSupplies.Add(supply);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag != "Supply")
        {
            return;
        }

        Supply supply = collider.gameObject.GetComponent<Supply>();
        m_activeSupplies.Remove(supply);
    }
}
