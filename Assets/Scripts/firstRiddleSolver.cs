using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class firstRiddleSolver : MonoBehaviour
{
    /*
     * First Riddle Satges:
     * 1 - Start the riddle - drop walls
     * 2 - Create explosion on the west waall
     * 3 - Reveal the spike ball inside the brick
     * 4 - Lift the ball and throw it on the north wall
     * 5 - The wall breaks into little tiny bricks pieces
     * 6 - Congrats! You finish the first riddle!
     */

    public UnityEvent startRidle;
    public UnityEvent warningPopUp;
    private bool isSceneSaved;

    public string GetScenePath()
    {
        string[] scenePath = SceneManager.GetActiveScene().path.Split(char.Parse("/"));
        return string.Join("/", scenePath);
    }

    public void SolveRidle()
    {
        string sceneSavedPath = GetScenePath();
        isSceneSaved = EditorSceneManager.SaveScene(SceneManager.GetActiveScene(), sceneSavedPath);
        Debug.Log("Saved Scene " + (isSceneSaved ? "Successful in" + sceneSavedPath : "Unsuccessful!"));
        warningPopUp.Invoke();
        startRidle.Invoke();


        Physics.autoSimulation = false;
        Physics.Simulate(Time.fixedDeltaTime);
        Physics.autoSimulation = true;
    }


    public void ResetRidleState()
    {
        string sceneSavedPath = GetScenePath();
        if (isSceneSaved && sceneSavedPath.Length > 0)
        {
            EditorSceneManager.OpenScene(GetScenePath(), OpenSceneMode.Single);
            Debug.Log("Scene Loaded!");
        }
        else 
        {
            Debug.Log("[Error! Failed to load scene");
        }

    }

}
