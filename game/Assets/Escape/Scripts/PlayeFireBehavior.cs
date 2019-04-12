using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayeFireBehavior : MonoBehaviour
{
    public enum WeaponType
    {
        Machinegun,
        Shotgun
    }

    public GameObject MachinegunPrefab;
    public GameObject ShotgunPrefab;
    public GameObject BulletPrefab;

    private PlayerIndex m_index = PlayerIndex.One;
    public PlayerIndex Index
    {
        set
        {
            m_index = value;
        }
    }

    private WeaponType m_weaponType = WeaponType.Machinegun;

    private float m_rotationSpeed = 290.0f;
    private float m_fireInterval = 0.0f;
    private float m_lastFireStamp = 0;

    private GameObject m_activeWeapon;

    void Start()
    {
        SetWeapon(WeaponType.Machinegun);
    }

    // Update is called once per frame
    void Update()
    {
        GamePadState state = GamePad.GetState(m_index);

        if (!state.IsConnected)
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

        if(state.Triggers.Right >= 0.5f && (fireDelta >= m_fireInterval))
        {
            m_lastFireStamp = Time.time;
            Fire();
        }
    }

    void Fire()
    {
        Transform fireSocket = transform.Find("FireSocket");
        Transform buttSocket = transform.Find("ButtSocket");

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
                    //#TODO:
                }
                break;

            default:
                Debug.Assert(false, "Unknown weapon type");
                break;
        }
    }

    public void SetWeapon(WeaponType type)
    {
        if(m_activeWeapon != null)
        {
            Destroy(m_activeWeapon);
            m_activeWeapon = null;
        }

        switch (type)
        {
            case WeaponType.Machinegun:
                m_activeWeapon = Instantiate(MachinegunPrefab, gameObject.transform);
                m_fireInterval = 1.0f / 20.0f;
                break;

            case WeaponType.Shotgun:
                m_activeWeapon = Instantiate(ShotgunPrefab, gameObject.transform);
                m_fireInterval = 1.0f;
                break;

            default:
                Debug.Assert(false, "Unknown weapon type");
                break;
        }
    }
}
