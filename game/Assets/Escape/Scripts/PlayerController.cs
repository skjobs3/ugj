using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public PlayerIndex Index;
    public GameObject GunPrefab;

    private GameObject m_gun;

    // Start is called before the first frame update
    void Start()
    {
        m_gun = Instantiate(GunPrefab, transform);
        m_gun.GetComponent<PlayerGunController>().Index = Index;
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
    }

    void ProcessMovement(GamePadState state)
    {
        Vector3 leftStick = new Vector3(state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y, 0.0f);
        Vector3 rightStick = new Vector3(state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y, 0.0f);

        Debug.Log("X:" + leftStick.x.ToString() + " Y:" + leftStick.y.ToString());

        Vector3 pos = gameObject.transform.position;
        pos += leftStick * Speed * Time.deltaTime;
        gameObject.transform.position = pos;
    }
}
