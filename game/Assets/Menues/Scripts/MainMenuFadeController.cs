using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.SceneManagement;

public class MainMenuFadeController : MonoBehaviour
{
    public GameObject ubiLogo;
    public GameObject teamTitle;
    public GameObject playButton;

    public Vector2 ubiLogoFadeDuration = new Vector2(0.0f, 1.0f);
    public Vector2 titleFadeDuration = new Vector2(1.0f, 2.0f);
    public Vector2 buttonFadeDuration = new Vector2(2.0f, 3.0f);

    private float sceneTime = 0.0f;
    private bool activated = false;

    void updateAlpha(GameObject spr, float alpha)
    {
        var color = spr.GetComponent<SpriteRenderer>().color;
        color.a = alpha;
        spr.GetComponent<SpriteRenderer>().color = color;
    }

    // Start is called before the first frame update
    void Start()
    {
        updateAlpha(ubiLogo, 0.0f);
        updateAlpha(teamTitle, 0.0f);
        updateAlpha(playButton, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        sceneTime += Time.deltaTime;
        if(sceneTime > ubiLogoFadeDuration.x && sceneTime < ubiLogoFadeDuration.y)
        {
            float duration = ubiLogoFadeDuration.y - ubiLogoFadeDuration.x;
            updateAlpha(ubiLogo, Mathf.Clamp(sceneTime - ubiLogoFadeDuration.x, 0.0f, duration));
        }

        if (sceneTime > titleFadeDuration.x && sceneTime < titleFadeDuration.y)
        {
            float duration = titleFadeDuration.y - titleFadeDuration.x;
            updateAlpha(teamTitle, Mathf.Clamp(sceneTime - titleFadeDuration.x, 0.0f, duration));
        }

        if (sceneTime > buttonFadeDuration.x && sceneTime < buttonFadeDuration.y)
        {
            float duration = buttonFadeDuration.y - buttonFadeDuration.x;
            updateAlpha(playButton, Mathf.Clamp(sceneTime - buttonFadeDuration.x, 0.0f, duration));
        }

        if(sceneTime >= buttonFadeDuration.y)
        {
            activated = true;
        }

        if(activated)
        {
            GamePadState state = GamePad.GetState(PlayerIndex.One);

            if(state.Buttons.A == ButtonState.Pressed)
            {
                TransitionInfo.Instance.NextSceneName = "Fly/Moon";
                SceneManager.LoadScene("Menues/Scenes/IntroCutScene");
            }
        }
    }
}
