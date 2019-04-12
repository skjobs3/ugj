using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 m_direction;
    public Vector3 Direction
    {
        set
        {
            m_direction = value;

            Vector3 dir = m_direction * -1.0f;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private float m_speed;
    public float Speed
    {
        set
        {
            m_speed = value;
        }
    }

    private float m_createTimeStamp;

    // Start is called before the first frame update
    void Start()
    {
        m_createTimeStamp = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float liveDelta = Time.time - m_createTimeStamp;

        if (liveDelta >= 5.0f)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 pos = gameObject.transform.position;
        pos += m_direction * m_speed * Time.deltaTime;
        gameObject.transform.position = pos;
    }
}
