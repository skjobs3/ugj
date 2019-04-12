using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class GamePlayerController : MonoBehaviour
{
    public int index;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerIndex playerIndex = PlayerIndex.One;
        if (index == 1)
        {
            playerIndex = PlayerIndex.Two;
        }
        GamePadState state = GamePad.GetState(playerIndex);

        if (state.IsConnected)
        {
            var pos = this.gameObject.transform.position;
            pos.x += state.ThumbSticks.Left.X / 10.0f;
            pos.y += state.ThumbSticks.Left.Y / 10.0f;
            this.gameObject.transform.position = pos;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
    }
}