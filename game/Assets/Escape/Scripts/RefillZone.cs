using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillZone : MonoBehaviour
{
    public SupplyZone.SupplyType Type;

    public GameObject AmmoOutfitPrefab;
    public GameObject FuelOutfitPrefab;

    private GameObject m_activeOutfit;

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
        
    }
}
