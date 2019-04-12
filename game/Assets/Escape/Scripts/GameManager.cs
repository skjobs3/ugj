using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            TransitionInfo.Instance.NextSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("Transition/GameOver");
        }
    }

    bool IsPlayerDead(PlayerController p)
    {
        return p.HP <= 0;
    }
}
