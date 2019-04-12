using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController Player1;
    public PlayerController Player2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlayerDead(Player1) || IsPlayerDead(Player2))
        {
            //#TODO: Death
        }
    }

    bool IsPlayerDead(PlayerController p1)
    {
        return p1.HP <= 0;
    }
}
