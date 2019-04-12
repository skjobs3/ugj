using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class GamePlayerController : MonoBehaviour
{
    public int index = 0;
    private bool m_isInAction = false;

    enum GameAction
    {
        none,
        activatorTriggered,
        activating,
        getterTriggered,
        getting,
        setterTriggered,
        setting,
    }

    private const int A_ButtonIndex = 0;
    private const int X_ButtonIndex = 1;
    private const int Y_ButtonIndex = 2;
    private const int B_ButtonIndex = 3;
    private GameAction m_currentAction = GameAction.none;
    private GamePadState m_gamePadState;
    private float m_progress = 0.0f;
    private int m_gettetAmount = 0;
    private Fly.Ship.Areas.Getter m_getter = null;
    private Fly.Ship.Areas.Setter m_setter = null;
    private Fly.Ship.Areas.Activator m_activator = null;
    private const int WantedCount = 100;

    [SerializeField]
    private System.Collections.Generic.List<SpriteRenderer> Buttons;

    [SerializeField]
    public SpriteRenderer ProgressBarBg;
    [SerializeField]
    public SpriteRenderer ProgressBar;

    // Start is called before the first frame update

    public int getPlayerIndex()
    {
        return index;
    }

    void ShowProgressBar()
    {
        ProgressBarBg.enabled = true;
        ProgressBar.enabled = true;
    }

    void HideProgressBar()
    {
        m_progress = 0.0f;
        ProgressBarBg.enabled = false;
        ProgressBar.enabled = false;
    }

    void SetProgress(float val)
    {
        var scale = ProgressBar.transform.localScale;
        scale.y = Mathf.Lerp(0.0f, 11.8f, val);
        ProgressBar.transform.localScale = scale;
    }

    void Start()
    {
        HideProgressBar();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerIndex playerIndex = PlayerIndex.One;
        if (index == 1)
        {
            playerIndex = PlayerIndex.Two;
        }
        m_gamePadState = GamePad.GetState(playerIndex);

        if (!m_gamePadState.IsConnected)
        {
            return;
        }
        if (!m_isInAction)
        {
            var pos = this.gameObject.transform.position;
            pos.x += m_gamePadState.ThumbSticks.Left.X / 10.0f;
            pos.y += m_gamePadState.ThumbSticks.Left.Y / 10.0f;
            this.gameObject.transform.position = pos;
        }

        processGameAcion();
    }

    private void processGameAcion()
    {
        if (m_currentAction == GameAction.none)
        {
            return;
        }

        switch (m_currentAction)
        {
            case GameAction.activatorTriggered:
                {
                    Buttons[A_ButtonIndex].enabled = true;
                    if (m_gamePadState.Buttons.A == ButtonState.Pressed)
                    {
                        Buttons[A_ButtonIndex].enabled = false;
                        m_currentAction = GameAction.activating;
                        ShowProgressBar();

                    }
                }
                break;
            case GameAction.activating:

                if (m_gamePadState.Buttons.A == ButtonState.Pressed)
                {
                    m_progress += 0.01f;
                    SetProgress(m_progress);
                    m_isInAction = true;
                    m_activator.Activate(this);
                }
                else
                {
                    HideProgressBar();
                    m_currentAction = GameAction.activatorTriggered;
                    m_isInAction = false;
                }
                break;
            case GameAction.getterTriggered:
                {
                    Buttons[X_ButtonIndex].enabled = true;
                    if (m_gamePadState.Buttons.X == ButtonState.Pressed)
                    {
                        Buttons[X_ButtonIndex].enabled = false;
                        m_currentAction = GameAction.getting;
                        ShowProgressBar();

                    }
                }
                break;
            case GameAction.getting:
                if (m_gamePadState.Buttons.X == ButtonState.Pressed)
                {
                    if (m_progress == 0.0f)
                    {
                        m_gettetAmount = 0;
                    }
                    m_isInAction = true;
                    m_progress += 0.01f;
                    SetProgress(m_progress);
                    int result = m_getter.Get(this, WantedCount);
                    if (result >= 0)
                    {
                        m_gettetAmount += result;
                    }
                    else
                    {
                        m_progress = 1.0f;
                    }

                    if (m_progress >= 1.0f)
                    {
                        HideProgressBar();
                        m_isInAction = false;
                    }
                }
                else
                {
                    HideProgressBar();
                    m_currentAction = GameAction.getterTriggered;
                    m_isInAction = false;
                }
                break;
            case GameAction.setterTriggered:
                    Buttons[Y_ButtonIndex].enabled = true;
                    if (m_gamePadState.Buttons.Y == ButtonState.Pressed)
                    {
                        Buttons[Y_ButtonIndex].enabled = false;
                        m_currentAction = GameAction.setting;
                        ShowProgressBar();

                }
                break;
            case GameAction.setting:
                if (m_gamePadState.Buttons.Y == ButtonState.Pressed)
                {
                    m_isInAction = true;
                    m_progress += 0.01f;
                    int amount = m_gettetAmount -= WantedCount;

                    if (amount >= 0)
                    {
                        m_setter.Set(this, WantedCount);
                    }
                    else
                    {
                        m_gettetAmount = 0;
                        m_progress = 1.0f;
                    }
                    SetProgress(m_progress);

                    if (m_progress >= 1.0f)
                    {
                        HideProgressBar();
                        m_isInAction = false;
                    }
                }
                else
                {
                    HideProgressBar();
                    m_currentAction = GameAction.setterTriggered;
                    m_isInAction = false;
                }
                break;
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Fly.Ship.Areas.Area Area = collision.gameObject.GetComponent<Fly.Ship.Areas.Area>();
        if (!Area)
        {
            return;
        }
        Buttons[A_ButtonIndex].enabled = false;
        Buttons[Y_ButtonIndex].enabled = false;
        Buttons[X_ButtonIndex].enabled = false;
        Fly.Ship.Areas.Getter Getter = Area as Fly.Ship.Areas.Getter;
        if (Getter)
        {
            m_currentAction = GameAction.getterTriggered;
            m_getter = Getter;
            return;
        }

        Fly.Ship.Areas.Setter Setter = Area as Fly.Ship.Areas.Setter;
        if (Setter)
        {
            m_currentAction = GameAction.setterTriggered;
            m_setter = Setter;
            return;
        }

        Fly.Ship.Areas.Activator Activator = Area as Fly.Ship.Areas.Activator;
        if (Activator)
        {
            m_activator = Activator;
            //Activator.Activate(this);
            m_currentAction = GameAction.activatorTriggered;
            return;
        }

    }

    void TriggerExit()
    {
        m_currentAction = GameAction.none;
        m_activator = null;
        m_getter = null;
        m_setter = null;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Fly.Ship.Areas.Area Area = collision.gameObject.GetComponent<Fly.Ship.Areas.Area>();
        if (!Area)
        {
            return;
        }
        
        Fly.Ship.Areas.Getter Getter = Area as Fly.Ship.Areas.Getter;
        if (Getter)
        {
            Buttons[X_ButtonIndex].enabled = false;
            TriggerExit();
            return;
        }
        
        Fly.Ship.Areas.Setter Setter = Area as Fly.Ship.Areas.Setter;
        if (Setter)
        {
            Buttons[Y_ButtonIndex].enabled = false;
            TriggerExit();
            return;
        }
        
        Fly.Ship.Areas.Activator Activator = Area as Fly.Ship.Areas.Activator;
        if (Activator)
        {
            Buttons[A_ButtonIndex].enabled = false;
            TriggerExit();
            return;
        }
    }

}