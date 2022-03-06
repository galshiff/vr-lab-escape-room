using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameDataLogger : MonoBehaviour
{
    TrialLogger trialLogger;

    [Serializable]
    public class User
    {
        public int _id;
        public int[] times;
        public int time_idx;
    }
    User user;
    List<string> columnList = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        user = new User
        {
            _id = getUserID(),
            times = new int[7],
            time_idx = 0
        };

        // define the names of the custom datapoints we want to log
        // trial number, participant ID, trial start/end time are logged automatically
/*        columnList = new List<string>
        { 
            "1st_riddle_start_time",
            "1st_riddle_end_time",
            "2nd_riddle_start_time",
            "2nd_riddle_end_time",
            "3rd_riddle_start_time",
            "3rd_riddle_end_time",
            "total_game_time",
        };*/

        // initialise trial logger
        trialLogger = GetComponent<TrialLogger>();
        trialLogger.Initialize(user._id.ToString(), columnList);
        StartLogging();
        FinishLogging();

        StartLogging();
        FinishLogging();

        StartLogging();
        FinishLogging();

        StartLogging();
        FinishLogging();
    }

    int getUserID()
    {
        // Create / Open folder for the JSON file
        string userIDFolderPath = Application.dataPath + "/StreamingAssets" + "/usersID";
        if (!Directory.Exists(userIDFolderPath))
        {
            Directory.CreateDirectory(userIDFolderPath);
        }

        string usersIDFilePath = userIDFolderPath + "/currentUser.json";
        user = new User
        {
            _id = 1
        };
        if (!File.Exists(usersIDFilePath))
        {
            // Write the init userID data to the JSON file
            File.WriteAllText(usersIDFilePath, JsonUtility.ToJson(user));
            return user._id;
        }
        
        string userIDJson = File.ReadAllText(usersIDFilePath);
        user = JsonUtility.FromJson<User>(userIDJson);
        user._id += 1; // Increament the userID by 1
        // Write the current userID data to the JSON file
        File.WriteAllText(usersIDFilePath, JsonUtility.ToJson(user));
        return user._id;
    }

    public void StartLogging()
    {
        trialLogger.StartTrial();
    }

    public void FinishLogging()
    {
        trialLogger.EndTrial();
    }

    void saveDataToTrial()
    {
        for (int i = 0; i < user.times.Length; i++)
        {
            string column = columnList[i];
            trialLogger.trial[column] = user.times[i].ToString();
        }
    }

}
