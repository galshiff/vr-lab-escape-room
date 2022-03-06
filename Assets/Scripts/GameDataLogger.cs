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
        public float[] times;
        public float time_idx;
        public float totalGameTime;
    }

    [Serializable]
    public class Leaderboard
    {
        public User winner;
        public User second_place;
        public User third_place;
    }

    User user;
    Leaderboard lead;
    List<string> columnList = new List<string>();
    float startGameTime, endGameTime;

    // Start is called before the first frame update
    void Start()
    {
        user = new User
        {
            _id = getUserID(),
            times = new float[7],
            time_idx = 0,
            totalGameTime = 0,
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
        startGameTime = Time.time;
        FinishLogging();

        StartLogging();
        FinishLogging();

        StartLogging();
        FinishLogging();
        endGameTime = Time.time + 4;

        FinishLoggingAfterWin();
    }

    int getUserID()
    {
        // Create / Open folder for the JSON file
        string userIDFolderPath = getDataLocation() + "/jsonFiles";
        if (!Directory.Exists(userIDFolderPath))
        {
            Directory.CreateDirectory(userIDFolderPath);
        }

        string usersIDFilePath = userIDFolderPath + "/currentUser.json";
        User user = new User
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

    public Leaderboard InitLeaderboard(User winner)
    {
        User second = new User
        {
            _id = -1,
            times = new float[7],
            time_idx = 0,
            totalGameTime = -1,
        };
        User third = new User
        {
            _id = -1,
            times = new float[7],
            time_idx = 0,
            totalGameTime = -1,
        };

        lead = new Leaderboard
        {
            winner = winner,
            second_place = second,
            third_place = third,
        };

        return lead;
    }

    public void StartLogging()
    {
        trialLogger.StartTrial();
    }

    public void FinishLogging()
    {
        trialLogger.EndTrial();
    }

    public void FinishLoggingAfterWin()
    {
        // Calc the total game time played by the usera
        StartLogging();
        FinishLogging();

        // Update the leaderboard file
        user.totalGameTime = endGameTime - startGameTime;
        saveToLeaderboardFile();
    }

    public void saveToLeaderboardFile()
    {
        // Create / Open folder for the JSON file
        string userIDFolderPath = getDataLocation() + "/jsonFiles";
        if (!Directory.Exists(userIDFolderPath))
        {
            Directory.CreateDirectory(userIDFolderPath);
        }

        string usersIDFilePath = userIDFolderPath + "/leaderboard.json";
        if (!File.Exists(usersIDFilePath))
        {
            Leaderboard initLead = InitLeaderboard(user);
            // Write the init userID data to the JSON file
            File.WriteAllText(usersIDFilePath, JsonUtility.ToJson(initLead));
        }
        else
        {
            string currLeadJson = File.ReadAllText(usersIDFilePath);
            Leaderboard currLead = JsonUtility.FromJson<Leaderboard>(currLeadJson);
            // Validate if we need to insert the user to the leaderboard
            User[] leadUsers = new User[] { currLead.third_place, currLead.second_place, currLead.winner };
            User[] newLeadUsers = new User[4];
            // The user score isn't high enough to make it to the leaderboard
            int userPos = getScorePosition(user.totalGameTime, currLead.winner.totalGameTime,
                                           currLead.second_place.totalGameTime, currLead.third_place.totalGameTime);
            newLeadUsers[userPos] = user;

            for (int i = 0; i < 3; i++)
            {
                if (i != userPos)
                {
                    newLeadUsers[i] = leadUsers[2 - i];
                }
                else 
                {
                    newLeadUsers[i+1] = leadUsers[2 - i];
                    userPos = i+1;
                }
            }

            Leaderboard newLead = new Leaderboard
            {
                winner = newLeadUsers[0],
                second_place = newLeadUsers[1],
                third_place = newLeadUsers[2]
            };
            // Write the current userID data to the JSON file
            File.WriteAllText(usersIDFilePath, JsonUtility.ToJson(newLead));
        }
    }

    int getScorePosition(float score, float high, float mid , float low)
    {
        if (score < low)
        {
            return 3;
        }

        if (score > low && score < mid)
        {
            return 2;
        }


        if (score > mid && score < high)
        {
            return 1;
        }

        // score > high
        return 0;
    }

    void saveDataToTrial()
    {
        for (int i = 0; i < user.times.Length; i++)
        {
            string column = columnList[i];
            trialLogger.trial[column] = user.times[i].ToString();
        }
    }


    string getDataLocation()
    {
        if (Application.isEditor)
        {
            return Application.dataPath + "/StreamingAssets";
        }

        return Application.persistentDataPath;
    }

}
