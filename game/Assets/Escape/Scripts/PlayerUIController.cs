using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public Slider HPSlider;
    public Text HPText;
    public Text Name;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetName(string name)
    {
        Name.text = name;
    }

    public void UpdateHP(int newValue, int total)
    {
        HPSlider.value = (float)newValue / (float)total;
        HPText.text = newValue.ToString();
    }
}
