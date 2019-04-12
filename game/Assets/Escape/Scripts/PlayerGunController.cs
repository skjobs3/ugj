using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerGunController : MonoBehaviour
{
    public PlayerIndex Index;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GamePadState state = GamePad.GetState(Index);

        if (!state.IsConnected)
        {
            return;
        }

        Vector3 stick = new Vector3(state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y, 0.0f);

        if (stick != Vector3.zero)
        {
            stick *= -1.0f;

            float angle = Mathf.Atan2(stick.y, stick.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
