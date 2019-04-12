using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class Continue : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GamePadState state = GamePad.GetState(PlayerIndex.One);

        if(state.Buttons.X == ButtonState.Pressed)
        {
            SceneManager.LoadScene(TransitionInfo.Instance.NextSceneName);
        }
    }
}
