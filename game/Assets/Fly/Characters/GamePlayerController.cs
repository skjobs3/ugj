﻿using System.Collections;
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
        Fly.Ship.Areas.Area Area = collision.gameObject.GetComponent<Fly.Ship.Areas.Area>();
        if (!Area)
        {
            return;
        }

        Fly.Ship.Areas.Getter Getter = Area as Fly.Ship.Areas.Getter;
        if (Getter)
        {
            int amount = Getter.Get(this, 100);
            return;
        }

        Fly.Ship.Areas.Setter Setter = Area as Fly.Ship.Areas.Setter;
        if (Setter)
        {
            Setter.Set(this, 100);
            return;
        }

        Fly.Ship.Areas.Activator Activator = Area as Fly.Ship.Areas.Activator;
        if (Activator)
        {
            Activator.Activate(this);
            return;
        }

    }
}