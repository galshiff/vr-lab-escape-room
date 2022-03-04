using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public UnityEvent onStart;
    public GameObject walls;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartBtn();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LeaderboardtBtn();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuitBtn();
        }
    }

    public void StartBtn()
    {
        // TODO: Destroy Main Menu
        // TODO: Enable OVR Player settings - "Enable Rotation" & "Enable Linear Move"
        mainMenu.SetActive(false);
        onStart.Invoke();
    }

    public void LeaderboardtBtn()
    {
        SceneManager.LoadScene("LeaderboardScene");
    }

    public void QuitBtn()
    {
        // TODO: Quit the Game
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                        Application.Quit();
        #endif
    }

    public void firstRiddle() 
    {
        walls.SetActive(true);
    }
}
