using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(firstRiddleSolver))]
public class firstRiddleSolverEditor : Editor
{
    private bool canSolvedRiddle = true;
    private bool canResetScene = false;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        firstRiddleSolver solver = (firstRiddleSolver)target;
        if (canSolvedRiddle && GUILayout.Button("Solve 1st riddle!"))
        {
            solver.SolveRidle();
            canSolvedRiddle = false;
            canResetScene = true;
        }

        if (canResetScene && GUILayout.Button("Reset 1st riddle"))
        {
            solver.ResetRidleState();
            canSolvedRiddle = true;
            canResetScene = false;
        }
    }
}
