using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RulesUIController : MonoBehaviour
{
    public Text AmmoText;
    public Text FuelText;
    public Text TimerText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAmmo(int current, int total)
    {
        AmmoText.text = current.ToString() + "/" + total.ToString();
    }

    public void UpdateFuel(int current, int total)
    {
        FuelText.text = current.ToString() + "/" + total.ToString();
    }

    public void UpdateTimer(int current)
    {
        TimerText.text = current.ToString();
    }
}
