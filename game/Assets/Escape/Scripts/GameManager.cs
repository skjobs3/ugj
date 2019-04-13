using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class GameManager : MonoBehaviour
{
    public RulesUIController RulesUIController;

    public PlayerController Player1;
    public PlayerController Player2;

    public string NextSceneName;

    public int AmmoToCollect;
    public int FuelToCollect;
    public int LevelTime;

    private int m_ammo = 0;
    private int m_fuel = 0;
    private int m_currentLevelTime;
    private float m_accumulatedTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_currentLevelTime = LevelTime;

        RulesUIController.UpdateAmmo(m_ammo, AmmoToCollect);
        RulesUIController.UpdateFuel(m_fuel, FuelToCollect);
        RulesUIController.UpdateTimer(m_currentLevelTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlayerDead(Player1) || IsPlayerDead(Player2))
        {
            FailGame();
        }

        // Update timer
        m_accumulatedTime += Time.deltaTime;

        if(m_accumulatedTime >= 1.0f)
        {
            int seconds = (int)m_accumulatedTime;

            m_accumulatedTime -= (float)seconds;
            m_currentLevelTime -= seconds;

            RulesUIController.UpdateTimer(m_currentLevelTime);

            if(m_currentLevelTime <= 0)
            {
                FailGame();
            }
        }

        //Cheat
        GamePadState state = GamePad.GetState(PlayerIndex.One);

        if(state.Buttons.Back == ButtonState.Pressed)
        {
            WinGame();
        }
    }

    bool IsPlayerDead(PlayerController p)
    {
        return p.HP <= 0;
    }

    void CheckWinConditions()
    {
        if(m_ammo >= AmmoToCollect && m_fuel >= FuelToCollect)
        {
            WinGame();
        }
    }

    public void CollectAmmo()
    {
        ++m_ammo;
        RulesUIController.UpdateAmmo(m_ammo, AmmoToCollect);
        CheckWinConditions();
    }

    public void CollectFuel()
    {
        ++m_fuel;
        RulesUIController.UpdateFuel(m_fuel, FuelToCollect);
        CheckWinConditions();
    }

    public void FailGame()
    {
        TransitionInfo.Instance.NextSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Transition/GameOver");
    }

    public void WinGame()
    {
        TransitionInfo.Instance.NextSceneName = NextSceneName;
        SceneManager.LoadScene("Transition/YouWin");
    }
}
