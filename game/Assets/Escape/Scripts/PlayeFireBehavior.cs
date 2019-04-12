using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayeFireBehavior : MonoBehaviour
{
    public GameObject BulletPrefab;

    private PlayerIndex m_index = PlayerIndex.One;
    public PlayerIndex Index
    {
        set
        {
            m_index = value;
        }
    }

    private float m_rotationSpeed = 0.0f;
    public float RotationSpeed
    {
        set
        {
            m_rotationSpeed = value;
        }
    }

    public int FireRate
    {
        set
        {
            m_fireInterval = 1.0f / (float)value;
        }
    }

    private float m_fireInterval = 0.0f;
    private float m_lastFireStamp = 0;

    // Start is called before the first frame update
    void Start()
    {
        
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
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetQuat, m_rotationSpeed * Time.deltaTime);
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

        Bullet bullet = Instantiate(BulletPrefab).GetComponent<Bullet>();
        bullet.transform.position = fireSocket.position;
        bullet.Direction = (fireSocket.position - buttSocket.position).normalized;
        bullet.Speed = 20;
    }
}
