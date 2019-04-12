using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class GamePlayerController : MonoBehaviour
{
    public int index;

    [SerializeField]
    private System.Collections.Generic.List<SpriteRenderer> Buttons;

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
        var component = collision.gameObject.GetComponent<Fly.Ship.Modules.Areas.Area>();
        if (component)
        {
            Buttons[0].enabled = true;
           // GetComponent
            //Debug.Log(component.gameObject.name);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Exit");
        var component = collision.gameObject.GetComponent<Fly.Ship.Modules.Areas.Area>();
        if (component)
        {
            Buttons[0].enabled = false;
            // GetComponent
            //Debug.Log(component.gameObject.name);
        }
    }
}