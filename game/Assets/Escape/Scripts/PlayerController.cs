using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerController : MonoBehaviour
{
    public float MovementSpeed;
    public PlayerIndex Index;

    public GameObject FireBehaviorPrefab;
    public GameObject AmmoSupplyPrefab;
    public GameObject FuelSupplyPrefab;

    public List<SupplyZone> m_supplyZones;

    public bool IsAbleToTakeSupply
    {
        get
        {
            return m_gun.activeSelf;
        }
    }

    public bool IsCarryingSupply
    {
        get
        {
            return m_supply != null;
        }
    }

    private GameObject m_gun;
    private GameObject m_supply;

    // Start is called before the first frame update
    void Start()
    {
        Transform socket = transform.Find("GunSocket");
        m_gun = Instantiate(FireBehaviorPrefab, socket);

        PlayeFireBehavior gun = m_gun.GetComponent<PlayeFireBehavior>();
        gun.Index = Index;
        gun.FireRate = 10;
        gun.RotationSpeed = 290.0f;

        SetGunEnabled(true);
    }

    // Update is called once per frame
    void Update()
    {
        GamePadState state = GamePad.GetState(Index);

        if(!state.IsConnected)
        {
            return;
        }

        ProcessMovement(state);

        if(state.Buttons.A == ButtonState.Pressed)
        {
            if(IsAbleToTakeSupply && !TakeSupplyFromTheGound())
            {
                TakeSupplyFromZone();
            }
        }
        else
        {
            if(IsCarryingSupply)
            {
                DropSupply();
            }
        }
    }

    void ProcessMovement(GamePadState state)
    {
        Vector3 leftStick = new Vector3(state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y, 0.0f);
        Vector3 rightStick = new Vector3(state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y, 0.0f);

        Vector3 pos = gameObject.transform.position;
        pos += leftStick * MovementSpeed * Time.deltaTime;
        gameObject.transform.position = pos;
    }

    void SetGunEnabled(bool enabled)
    {
        m_gun.SetActive(enabled);
    }

    bool TakeSupplyFromZone()
    {
        foreach(SupplyZone zone in m_supplyZones)
        {
            var playerCollider = gameObject.GetComponent<Collider2D>();

            if(zone.GetComponent<Collider2D>().IsTouching(playerCollider))
            {
                GameObject supply = null;

                switch (zone.Type)
                {
                    case SupplyZone.SupplyType.Ammo:
                        supply = Instantiate(AmmoSupplyPrefab);
                        break;

                    case SupplyZone.SupplyType.Fuel:
                        supply = Instantiate(FuelSupplyPrefab);
                        break;

                    default:
                        Debug.Assert(false, "Unknown supply type");
                        break;
                }

                supply.GetComponent<Supply>().Type = zone.Type;
                supply.tag = "Supply";

                TakeSupply(supply);
                return true;
            }
        }

        return false;
    }

    bool TakeSupplyFromTheGound()
    {
        var supplies = GameObject.FindGameObjectsWithTag("Supply");

        if (supplies.Length == 0)
        {
            return false;
        }

        foreach(GameObject supply in supplies)
        {
            var playerCollider = gameObject.GetComponent<Collider2D>();

            if (supply.GetComponent<Collider2D>().IsTouching(playerCollider))
            {
                Debug.Log("Pickuing up supply from ground");
                //#TODO: Pickup this supply
                return true;
            }
        }

        return false;
    }

    void TakeSupply(GameObject supply)
    {
        Transform socket = gameObject.transform.Find("SupplySocket");
        supply.transform.SetParent(socket);
        supply.transform.localPosition = Vector3.zero;
        supply.GetComponent<Supply>().IsTaken = true;

        m_supply = supply;
        SetGunEnabled(false);
    }

    void DropSupply()
    {
        if(m_supply != null)
        {
            m_supply.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 350.0f);
            m_supply.GetComponent<Supply>().IsTaken = false;
            m_supply.transform.SetParent(null);
            m_supply = null;

            SetGunEnabled(true);
        }
    }
}
