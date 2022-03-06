using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    [Serializable]
    public class User
    {
        public int _id;
        public float[] times;
        public float time_idx;
        public float totalGameTime;
    }

    [Serializable]
    public class Leaderboardscores
    {
        public User winner;
        public User second_place;
        public User third_place;
    }

    Leaderboardscores lead;

    // Start is called before the first frame update
    void Start()
    {
        lead = getLeaderboard();
        User[] leadUsers = new User[] { lead.winner, lead.second_place, lead.third_place };
        for (int i = 0; i < leadUsers.Length; i++)
        {
            User currUser = leadUsers[i];
            TMP_Text podiumPlaceTxt = GameObject.Find("Canvas").gameObject.transform.GetChild(i).GetComponent<TMP_Text>();
            podiumPlaceTxt.text = GetPodiumText(currUser._id, currUser.totalGameTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    Leaderboardscores getLeaderboard()
    {
        // Open folder for the JSON file
        string userIDFolderPath = Application.dataPath + "/StreamingAssets" + "/jsonFiles";
        if (!Directory.Exists(userIDFolderPath))
        {
            return null; // not exist..
        }

        string usersIDFilePath = userIDFolderPath + "/leaderboard.json";
        if (!File.Exists(usersIDFilePath))
        {
            // TODO
        }

        string usersLeaderboard = File.ReadAllText(usersIDFilePath);
        Leaderboardscores leadboard = JsonUtility.FromJson<Leaderboardscores>(usersLeaderboard);
        
        return leadboard;
    }

    string GetPodiumText(int userID, float score)
    {
        if (userID < 0 || score < 0)
        {
            return "\nNo Result";
        }

        return "User " + userID + "\n" + score + " Seconds";
    }


}
