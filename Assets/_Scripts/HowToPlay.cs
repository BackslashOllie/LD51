using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class HowToPlay : MonoBehaviour
{
    public Image gamePadControls;
    public Image keyboardControls;
    public TextMeshProUGUI switchText;
    // Start is called before the first frame update

    public void SwitchSchemes()
    {
        if (gamePadControls.isActiveAndEnabled)
        {
            gamePadControls.gameObject.SetActive(false);
            keyboardControls.gameObject.SetActive(true);
            switchText.text = switchText.text.Replace("Keyboard / Mouse", "Gamepad");
            
        }
        else
        {
            gamePadControls.gameObject.SetActive(true);
            keyboardControls.gameObject.SetActive(false);
            switchText.text = switchText.text.Replace("Gamepad", "Keyboard / Mouse");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
