using System.Collections;
using UnityEngine;
using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.Common;
using UnityEngine.UI;
using TMPro;

public class DatabaseManager : MonoBehaviour
{
    public TextMeshProUGUI[] usernameTexts;
    public TextMeshProUGUI[] mapTexts;
    public TextMeshProUGUI[] difficultyTexts;
    public TextMeshProUGUI[] timeTexts;
    public TextMeshProUGUI[] scoreTexts;

    private string connectionString;
    private MySqlConnection dbConnection;

    private void Start()
    {
        string databaseName = "sql7621831";
        string host = "sql7.freemysqlhosting.net";
        string user = "sql7621831";
        string password = "s5iq4Hi5Sk";

        connectionString = $"Server={host};Database={databaseName};Uid={user};Pwd={password};";
        ConnectToDatabase();

        if (dbConnection.State == ConnectionState.Open)
        {
            FetchScoreboards();
        }
    }

    private void ConnectToDatabase()
    {
        try
        {
            dbConnection = new MySqlConnection(connectionString);
            dbConnection.Open();
            Debug.Log("Connected to the database!");
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to connect to the database: " + e.Message);
        }
    }

    private void FetchScoreboards()
    {
        string query = "SELECT username, map, difficulty, time, score FROM scoreboard ORDER BY score DESC LIMIT 5";
        MySqlCommand cmd = new MySqlCommand(query, dbConnection);

        try
        {
            MySqlDataReader reader = cmd.ExecuteReader();

            int index = 0;
            while (reader.Read() && index < usernameTexts.Length)
            {
                string username = reader.GetString(0);
                string map = reader.GetString(1);
                string difficulty = reader.GetString(2);

                if(difficulty == "Easy")
                {
                    difficulty = "<color=green>Easy</color>";
                } else if (difficulty == "Medium")
                {
                    difficulty = "<color=yellow>Medium</color>";
                } else
                {
                    difficulty = "<color=red>Hard</color>";
                }

                float timeInSeconds = reader.GetFloat(3);
                int score = reader.GetInt32(4);

                TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);
                string timeString = $"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";

                usernameTexts[index].text = username;
                mapTexts[index].text = map;
                difficultyTexts[index].text = difficulty;
                timeTexts[index].text = timeString;
                scoreTexts[index].text = score.ToString();

                index++;
            }

            reader.Close();
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to fetch scoreboards: " + e.Message);
        }
    }


    private void OnDestroy()
    {
        if (dbConnection != null && dbConnection.State == ConnectionState.Open)
        {
            dbConnection.Close();
            Debug.Log("Disconnected from the database.");
        }
    }

    public void TogglePanel()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

}
