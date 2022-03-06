using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Keyboard : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject normalButtons;
    public GameObject capsButtons;
    public UnityEvent onWin;
    public UnityEvent onFinish;
    private bool caps;


    // Start is called before the first frame update
    void Start()
    {
        caps = false;
    }

    public void InsertChar(string c)
    {
        inputField.text += c;
    }

    public void DeleteChar()
    {
        if (inputField.text.Length > 0)
        {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
    }

    public void Clear()
    {
        if (inputField.text.Length > 0)
        {
            inputField.text = "";
        }
    }

    public void Enter()
    {
        if (inputField.text.Length > 0)
        {
            if (inputField.text == "8")
            {
                inputField.text = "Correct!";
                onFinish.Invoke();
                onWin.Invoke();
            }
            else
            {
                inputField.text = "Wrong!";
            }
        }

    }

    public void InsertSpace()
    {
        inputField.text = " ";
    }

    public void CapsPerssed()
    {
        if (!caps)
        {
            normalButtons.SetActive(false);
            capsButtons.SetActive(true);
            caps = true;
        }
        else
        {
            normalButtons.SetActive(true);
            capsButtons.SetActive(false);
            caps = false;
        }
    }


}
