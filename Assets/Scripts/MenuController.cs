using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("LeaderboardScene");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuitBtn();
        }
    }

    public void StartBtn()
    {
        // TODO: Destruct Main Menu
        // TODO: Enable OVR Player settings - "Enable Rotation" & "Enable Linear Move"
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
}
