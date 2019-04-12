using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GamePadState state = GamePad.GetState(PlayerIndex.One);

        if(state.Buttons.A == ButtonState.Pressed)
        {
            Debug.Log("a1");
        }

        state = GamePad.GetState(PlayerIndex.Two);

        if (state.Buttons.A == ButtonState.Pressed)
        {
            Debug.Log("a2");
        }
    }
}
