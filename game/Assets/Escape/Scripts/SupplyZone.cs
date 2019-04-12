using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyZone : MonoBehaviour
{
    private List<PlayerController> m_activePlayers = new List<PlayerController>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool anyPlayerValid = false;

        foreach(PlayerController player in m_activePlayers)
        {
            if(player.IsAbleToTakeSupply)
            {
                anyPlayerValid = true;
                break;
            }
        }

        SetHintVisible(anyPlayerValid);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag != "Player")
        {
            return;
        }

        m_activePlayers.Add(collider.gameObject.GetComponent<PlayerController>());
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag != "Player")
        {
            return;
        }

        m_activePlayers.Remove(collider.gameObject.GetComponent<PlayerController>());
    }

    void SetHintVisible(bool visible)
    {
        gameObject.transform.Find("Hint").gameObject.SetActive(visible);
    }
}
