using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerController : MonoBehaviour
{
    public float MovementSpeed;
    public PlayerIndex Index;

    public GameObject FireBehaviorPrefab;
    public GameObject SupplyPrefab;

    public List<SupplyZone> m_supplyZones;

    public bool IsAbleToTakeSupply
    {
        get
        {
            return GetComponent<WeaponManager>().IsWeaponActive;
        }
    }

    public bool IsCarryingSupply
    {
        get
        {
            return m_supply != null;
        }
    }

    private int m_hp = 100;
    public int HP
    {
        get
        {
            return m_hp;
        }
    }

    private GameObject m_supply;

    private bool m_leftDPadWasPressed = false;
    private bool m_rightDPadWasPressed = false;

    // Start is called before the first frame update
    void Start()
    {

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
        ProcessSupplies(state);
        ProcessWeaponChanging(state);
    }

    void ProcessMovement(GamePadState state)
    {
        Vector3 leftStick = new Vector3(state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y, 0.0f);
        Vector3 rightStick = new Vector3(state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y, 0.0f);

        Vector3 pos = gameObject.transform.position;
        pos += leftStick * MovementSpeed * Time.deltaTime;
        gameObject.transform.position = pos;
    }

    void ProcessSupplies(GamePadState state)
    {
        if (state.Buttons.A == ButtonState.Pressed)
        {
            if(IsAbleToTakeSupply && !TakeSupplyFromTheGound())
            {
                TakeSupplyFromZone();
            }
        }
        else
        {
            if (IsCarryingSupply)
            {
                DropSupply(state);
            }
        }
    }

    void ProcessWeaponChanging(GamePadState state)
    {
        WeaponManager manager = GetComponent<WeaponManager>();

        if(!manager.IsWeaponActive)
        {
            return;
        }

        if (!IsCarryingSupply)
        {
            if (state.DPad.Left == ButtonState.Pressed)
            {
                m_leftDPadWasPressed = true;
            }

            if (state.DPad.Right == ButtonState.Pressed)
            {
                m_rightDPadWasPressed = true;
            }

            if (state.DPad.Left == ButtonState.Released && m_leftDPadWasPressed)
            {
                m_leftDPadWasPressed = false;
                manager.PrevWeapon();
            }

            if (state.DPad.Right == ButtonState.Released && m_rightDPadWasPressed)
            {
                m_rightDPadWasPressed = false;
                manager.NextWeapon();
            }
        }
    }

    void SetGunEnabled(bool enabled)
    {
        GetComponent<WeaponManager>().IsWeaponActive = enabled;
    }

    bool TakeSupplyFromZone()
    {
        foreach(SupplyZone zone in m_supplyZones)
        {
            var playerCollider = gameObject.GetComponent<Collider2D>();

            if(zone.IsInsidePickupArea(playerCollider))
            {
                GameObject supply = Instantiate(SupplyPrefab);
                supply.GetComponent<Supply>().SetType(zone.Type);
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

            if (supply.GetComponent<Supply>().IsInsidePickupArea(playerCollider))
            {
                TakeSupply(supply);
                return true;
            }
        }

        return false;
    }

    void TakeSupply(GameObject supply)
    {
        m_supply = supply;
        m_supply.GetComponent<Supply>().Take(Vector3.zero, gameObject.transform.Find("SupplySocket"));
        SetGunEnabled(false);
    }

    void DropSupply(GamePadState state)
    {
        if(m_supply != null)
        {
            Vector3 stick = new Vector3(state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y, 0.0f);

            if(stick == Vector3.zero)
            {
                stick = Vector3.right;
            }

            m_supply.GetComponent<Supply>().Drop(stick.normalized * 350.0f);
            m_supply = null;

            SetGunEnabled(true);
        }
    }

    public void Hit(int count)
    {
        m_hp -= count;
        //#TODO: Check death conditions
    }
}

