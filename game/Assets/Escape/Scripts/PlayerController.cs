using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerController : MonoBehaviour
{
    public float MovementSpeed;
    public PlayerIndex Index;
    public GameObject FireBehaviorPrefab;
    public GameObject CarryBehaviorPrefab;

    private GameObject m_behavior;

    // Start is called before the first frame update
    void Start()
    {
        SetFireBehavior();
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

        Vector3 pos = gameObject.transform.position;
        pos += leftStick * MovementSpeed * Time.deltaTime;
        gameObject.transform.position = pos;
    }

    void SetFireBehavior()
    {
        Transform socket = transform.Find("FireBehaviorSocket");
        m_behavior = Instantiate(FireBehaviorPrefab, socket);

        PlayeFireBehavior gun = m_behavior.GetComponent<PlayeFireBehavior>();
        gun.Index = Index;
        gun.FireRate = 10;
        gun.RotationSpeed = 290.0f;
    }

    void SetCarryBehavior()
    {

    }
}
