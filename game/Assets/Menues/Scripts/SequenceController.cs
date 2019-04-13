using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
public class SequenceController : MonoBehaviour
{
    public float singleSequenceFadeDuration = 1.0f;
    public float singleSequenceFadeInStart = 0.0f;
    public float singleSequenceFadeOutStart = 4.0f;
    public float singleSequnceDuration = 5.0f;
    public List<GameObject> sequences = new List<GameObject>();

    private float sequenceTimer = 0.0f;
    private int currentSequenceIndex = 0;
    private bool ended = false;
    private bool processingSinglePress = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void SetAllSequencesInactive()
    {
        foreach (var sequence in sequences)
        {
            sequence.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetAllSequencesInactive();
        sequenceTimer += Time.deltaTime;

        if (currentSequenceIndex < sequences.Count)
        {
            if (sequenceTimer < singleSequnceDuration)
            {
                float fadeRatio = 1.0f;

                if (sequenceTimer < singleSequenceFadeInStart + singleSequenceFadeDuration && sequenceTimer > singleSequenceFadeInStart)
                {
                    fadeRatio = Mathf.Clamp((sequenceTimer - singleSequenceFadeInStart) / singleSequenceFadeDuration, 0.0f, 1.0f);
                }
                else if (sequenceTimer < singleSequenceFadeOutStart + singleSequenceFadeDuration && sequenceTimer > singleSequenceFadeOutStart)
                {
                    fadeRatio = Mathf.Clamp(singleSequenceFadeDuration - (sequenceTimer - singleSequenceFadeOutStart) / singleSequenceFadeDuration, 0.0f, 1.0f);
                }

                var spriteColor = sequences[currentSequenceIndex].transform.Find("SequenceSprite").GetComponent<SpriteRenderer>().color;
                spriteColor.a = fadeRatio;

                sequences[currentSequenceIndex].SetActive(true);
                sequences[currentSequenceIndex].transform.Find("SequenceSprite").GetComponent<SpriteRenderer>().color = spriteColor;
                sequences[currentSequenceIndex].transform.Find("Canvas").GetComponent<CanvasGroup>().alpha = fadeRatio;
            }
            else
            {
                sequenceTimer = 0.0f;
                currentSequenceIndex++;
            }
        }
        else
        {
            ended = true;
        }

        GamePadState state = GamePad.GetState(PlayerIndex.One);
        if(state.Buttons.A == ButtonState.Pressed && !processingSinglePress)
        {
            processingSinglePress = true;
            sequenceTimer = 0.0f;
            currentSequenceIndex++;
        }

        if(state.Buttons.A == ButtonState.Released && processingSinglePress)
        {
            processingSinglePress = false;
        }
    }
}
