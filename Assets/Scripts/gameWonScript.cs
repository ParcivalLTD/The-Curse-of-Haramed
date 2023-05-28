using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameWonScript : MonoBehaviour
{
    private GameManagerBehavior gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        
    }

    void Update()
    {
        if(gameManager.gameWon)
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
            PlayerPrefs.SetInt((SceneManager.GetActiveScene().name + "_" + PlayerPrefs.GetInt("difficulty")).ToString(), 1);
        }
    }

    public void freeplay()
    {
        transform.Find("thing").gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void returnToMenu()
    {
        string map;
        if (PlayerPrefs.GetInt("currentPanel") == 0)
        {
            map = "Crusts and Carnage";
        }
        else if (PlayerPrefs.GetInt("currentPanel") == 1)
        {
            map = "Chalkboard Chowdown";
        }
        else
        {
            map = "Quranic Quandary";
        }

        string username = PlayerPrefs.GetString("PlayerName");

        string[] timeParts = GameObject.Find("Timelabel").GetComponent<Text>().text.Split(':');
        int minutes = int.Parse(timeParts[0]);
        int seconds = int.Parse(timeParts[1]);
        float time = minutes * 60 + seconds;

        string difficulty;
        if (PlayerPrefs.GetInt("difficulty") == 0)
        {
            difficulty = "Easy";
        }
        else if (PlayerPrefs.GetInt("difficulty") == 1)
        {
            difficulty = "Medium";
        }
        else
        {
            difficulty = "Hard";
        }

        int score = int.Parse(GameObject.Find("WaveLabel").GetComponent<Text>().text) * (int)(1 + PlayerPrefs.GetInt("difficulty") * 0.7f);

        Time.timeScale = 1.0f;
        GameObject gameOverText = GameObject.FindGameObjectWithTag("GameOver");
        gameOverText.GetComponent<Animator>().SetBool("gameOver", true);

        SaveScoreboard(username, map, difficulty, time, score);
    }

    private void SaveScoreboard(string username, string map, string difficulty, float time, int score)
    {
        string databaseName = "sql7621831";
        string host = "sql7.freemysqlhosting.net";
        string user = "sql7621831";
        string password = "s5iq4Hi5Sk";

        string connectionString = $"Server={host};Database={databaseName};Uid={user};Pwd={password};";

        using (MySqlConnection dbConnection = new MySqlConnection(connectionString))
        {
            try
            {
                dbConnection.Open();

                string query = "INSERT INTO scoreboard (username, map, difficulty, time, score) VALUES (@username, @map, @difficulty, @time, @score)";
                MySqlCommand cmd = new MySqlCommand(query, dbConnection);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@map", map);
                cmd.Parameters.AddWithValue("@difficulty", difficulty);
                cmd.Parameters.AddWithValue("@time", time);
                cmd.Parameters.AddWithValue("@score", score);

                cmd.ExecuteNonQuery();
                Debug.Log("Scoreboard entry saved!");
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to save scoreboard entry: " + e.Message);
            }
        }
    }
}
