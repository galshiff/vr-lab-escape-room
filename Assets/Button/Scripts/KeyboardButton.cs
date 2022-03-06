using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class KeyboardButton : MonoBehaviour
{
    Keyboard keyboard;
    TextMeshProUGUI buttonTxt;

    // Start is called before the first frame update
    void Start()
    {
        keyboard = GetComponentInParent<Keyboard>();
        buttonTxt = GetComponentInChildren<TextMeshProUGUI>();
        if (buttonTxt.text.Length == 1)
        {
            NameToButtonTxt();
            GetComponentInChildren<ButtonVR>().onRelease.AddListener(delegate { 
                keyboard.InsertChar(buttonTxt.text);
            });
        }
    }

    private void NameToButtonTxt()
    {
        buttonTxt.text = gameObject.name;
    }
}
