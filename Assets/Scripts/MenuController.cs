using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartBtn()
    {
        // TODO: Destruct Main Menu
        // TODO: Enable OVR Player settings - "Enable Rotation" & "Enable Linear Move"
    }

    public void LeadboardtBtn()
    {
        // TODO: Switch to Leadboard Scene
        //SceneManager.LoadScene()
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
