using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TrialLogger : MonoBehaviour {

    public int currentTrialNumber = 0;    
    List<string> header;
    [HideInInspector]
    public Dictionary<string, string> trial;
    [HideInInspector]
    public string outputFolder;

    bool trialStarted = false;
    string ppid;
    string dataOutputPath;
    List<string> output;
    readonly string[] riddles = new string[] { "1st Riddle",
                                               "2nd Riddle",
                                               "3rd Riddle",
                                               "Total Game Time"
    };

    // Use this for initialization
    void Awake () {
        outputFolder = Application.dataPath + "/StreamingAssets" + "/output";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }
    }
	

	// Update is called once per frame
	void Update () {
		
	}

    public void Initialize(string participantID, List<string> customHeader)
    {
        outputFolder = Application.dataPath + "/StreamingAssets" + "/output";
        ppid = participantID;
        header = customHeader;
        InitHeader();
        InitDict();
        output = new List<string>();
        output.Add(string.Join(",", header.ToArray()));
        dataOutputPath = outputFolder + "/user_" + participantID + "_timing_data.csv";
    }

    private void InitHeader()
    {
        header.Insert(0, "riddle");
        header.Insert(1, "userID");
        header.Insert(2, "start_time");
        header.Insert(3, "end_time");
    }

    private void InitDict()
    {
        trial = new Dictionary<string, string>();
        foreach (string value in header)
        {
            trial.Add(value, "");
        }
    }

    public void StartTrial(float time)
    {
        trialStarted = true;
        InitDict();
        trial["riddle"] = riddles[currentTrialNumber];
        currentTrialNumber += 1;
        trial["userID"] = ppid;
        if (time < 0)
        {
            time = Time.time;
        }
        trial["start_time"] = time.ToString();
    }

    public void EndTrial(float time)
    {
        if (output != null && dataOutputPath != null)
        {
            if (trialStarted)
            {
                if (time < 0)
                {
                    time = Time.time;
                }
                trial["end_time"] = time.ToString();
                output.Add(FormatTrialData());
                trialStarted = false;
            }
            else Debug.LogError("Error ending trial - Trial wasn't started properly");

        }
        else Debug.LogError("Error ending trial - TrialLogger was not initialsed properly");
    }

    private string FormatTrialData()
    {
        List<string> rowData = new List<string>();
        foreach (string value in header)
        {
            rowData.Add(trial[value]);
        }
        return string.Join(",", rowData.ToArray());
    }

    private void OnApplicationQuit()
    {
        // TODO: Calc total game time
        // Insert to the last position (7) the total game time
        // int totalGameTime = user.times[5] - user.times[0];
        // user.times[user.time_idx] = totalGameTime;
        // saveDataToTrial();
        if (output != null && dataOutputPath != null)
        {
            File.WriteAllLines(dataOutputPath, output.ToArray());
            Debug.Log(string.Format("Saved data to {0}.", dataOutputPath));
        }
        else Debug.LogError("Error saving data - TrialLogger was not initialsed properly");
        
    }
}
