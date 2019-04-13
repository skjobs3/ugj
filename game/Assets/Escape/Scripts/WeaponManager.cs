using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class WeaponManager : MonoBehaviour
{
    public enum WeaponType
    {
        Machinegun,
        Shotgun
    }

    public PlayerIndex Index;

    public GameObject MachinegunPrefab;
    public GameObject ShotgunPrefab;
    public GameObject BulletPrefab;

    private bool m_weaponActive = true;
    public bool IsWeaponActive
    {
        get
        {
            return m_weaponActive;
        }

        set
        {
            m_weaponActive = value;
            m_activeWeapon.SetActive(m_weaponActive);
        }
    }

    private WeaponType m_weaponType = WeaponType.Machinegun;
    public WeaponType ActiveWeaponType
    {
        get
        {
            return m_weaponType;
        }
    }

    private float m_rotationSpeed = 290.0f;
    private float m_fireInterval = 0.0f;
    private float m_lastFireStamp = 0;

    private List<WeaponType> m_weaponsQueue = new List<WeaponType>()
    {
        WeaponType.Machinegun,
        WeaponType.Shotgun
    };

    private int m_activeSelectedWeaponIndex = 0;

    private GameObject m_activeWeapon;
    public GameObject ActiveWeapon
    {
        get
        {
            return m_activeWeapon;
        }
    }

    void Start()
    {
        SetWeapon(WeaponType.Machinegun);
    }

    // Update is called once per frame
    void Update()
    {
        GamePadState state = GamePad.GetState(Index);

        if (!state.IsConnected || !IsWeaponActive)
        {
            return;
        }

        // Rotation
        Vector3 stick = new Vector3(state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y, 0.0f);

        if (stick != Vector3.zero)
        {
            stick *= -1.0f;

            float angle = Mathf.Atan2(stick.y, stick.x) * Mathf.Rad2Deg;
            Quaternion targetQuat = Quaternion.AngleAxis(angle, Vector3.forward);
            m_activeWeapon.transform.rotation = Quaternion.RotateTowards(m_activeWeapon.transform.rotation, targetQuat, m_rotationSpeed * Time.deltaTime);
        }

        // Fire
        float fireDelta = Time.time - m_lastFireStamp;

        if (state.Triggers.Right >= 0.5f && (fireDelta >= m_fireInterval))
        {
            m_lastFireStamp = Time.time;
            Fire();
        }
    }

    public void SetWeapon(WeaponType type)
    {
        Quaternion rotation = Quaternion.identity;

        if (m_activeWeapon != null)
        {
            rotation = m_activeWeapon.transform.rotation;
            Destroy(m_activeWeapon);
            m_activeWeapon = null;
        }

        Transform socket = gameObject.transform.Find("GunSocket");
        m_weaponType = type;

        switch (type)
        {
            case WeaponType.Machinegun:
                m_activeWeapon = Instantiate(MachinegunPrefab, socket);
                m_fireInterval = 1.0f / 15.0f;
                break;

            case WeaponType.Shotgun:
                m_activeWeapon = Instantiate(ShotgunPrefab, socket);
                m_fireInterval = 0.2f;
                break;

            default:
                Debug.Assert(false, "Unknown weapon type");
                break;
        }

        m_activeWeapon.transform.rotation = rotation;
    }

    public void NextWeapon()
    {
        m_activeSelectedWeaponIndex = (m_activeSelectedWeaponIndex + 1) % m_weaponsQueue.Count;
        SetWeapon(m_weaponsQueue[m_activeSelectedWeaponIndex]);
    }

    public void PrevWeapon()
    {
        if(m_activeSelectedWeaponIndex == 0)
        {
            m_activeSelectedWeaponIndex = m_weaponsQueue.Count - 1;
        }
        else
        {
            --m_activeSelectedWeaponIndex;
        }

        SetWeapon(m_weaponsQueue[m_activeSelectedWeaponIndex]);
    }

    private void Fire()
    {
        Transform fireSocket = m_activeWeapon.transform.Find("FireSocket");
        Transform buttSocket = m_activeWeapon.transform.Find("ButtSocket");

        switch (m_weaponType)
        {
            case WeaponType.Machinegun:
                {
                    Bullet bullet = Instantiate(BulletPrefab).GetComponent<Bullet>();
                    bullet.transform.position = fireSocket.position;
                    bullet.Direction = (fireSocket.position - buttSocket.position).normalized;
                    bullet.Speed = 20;
                }
                break;

            case WeaponType.Shotgun:
                {
                    List<Transform> sockets = new List<Transform>();

                    for(int i = 1; i < 5; ++i)
                    {
                        sockets.Add(m_activeWeapon.transform.Find("DirSocket" + i));
                    }

                    int freeSocketIndices = 0;

                    for (int i = 0; i < Random.Range(2, 5); ++i)
                    {
                        Transform dirSocket = sockets[freeSocketIndices];

                        Bullet bullet = Instantiate(BulletPrefab).GetComponent<Bullet>();
                        bullet.transform.position = fireSocket.position;
                        bullet.Direction = (dirSocket.gameObject.transform.position - fireSocket.gameObject.transform.position).normalized;
                        bullet.Speed = 20;

                        ++freeSocketIndices;
                    }
                }
                break;

            default:
                Debug.Assert(false, "Unknown weapon type");
                break;
        }
    }
}