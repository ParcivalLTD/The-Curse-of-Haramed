using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pausegame : MonoBehaviour
{
    public bool isPaused = false;
    private GameObject pauseImage;
    public Sprite pausedSprite;
    public Sprite normalSprite;
    public GameObject pauseOverlay;
    private bool gameOver;

    public Toggle fullscreenToggle;
    public Slider sfxSlider;
    public Slider musicSlider;
    public Toggle sfxMuteToggle;
    public Toggle musicMuteToggle;
    public GameObject trackNameObj;

    public bool isFullscreen;
    private float sfxVolume;
    private float musicVolume;
    private bool isSfxMuted;
    private bool isMusicMuted;

    public GameObject pauseOverlayBackground;

    public bool getFullscreenBool()
    {
        return isFullscreen;
    }

    public void Update()
    {
        trackNameObj.GetComponent<TextMeshProUGUI>().text = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicPlayer>().GetCurrentTrackName();

        if (isPaused)
        {
            if (Input.anyKeyDown && !Input.GetMouseButton(0) && !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonUp(0) && !Input.GetMouseButtonDown(1) && !Input.GetMouseButton(1) && !Input.GetMouseButtonUp(1))
            {
                TogglePause();
            }
        }

    }

    void Start()
    {
        pauseImage = transform.Find("pauseImage").gameObject;
        pauseOverlay.SetActive(false);
        gameOver = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>().gameOver;

        if (!PlayerPrefs.HasKey("isFullscreen"))
        {
            PlayerPrefs.SetInt("isFullscreen", 1);
            isFullscreen = true;
        }
        else
        {
            isFullscreen = PlayerPrefs.GetInt("isFullscreen", 1) == 1 ? true : false;
        }

        sfxVolume = PlayerPrefs.GetFloat("sfxVolume", 1f);
        musicVolume = PlayerPrefs.GetFloat("musicVolume", 1f);
        isSfxMuted = PlayerPrefs.GetInt("isSfxMuted", 0) == 1 ? true : false;
        isMusicMuted = PlayerPrefs.GetInt("isMusicMuted", 0) == 1 ? true : false;

        fullscreenToggle.isOn = isFullscreen;

        sfxSlider.value = sfxVolume;
        musicSlider.value = musicVolume;
        sfxMuteToggle.isOn = isSfxMuted;
        musicMuteToggle.isOn = isMusicMuted;

        ApplySettings();
    }


    private void ApplySettings()
    {
        Screen.fullScreen = isFullscreen;

        AudioListener.volume = isSfxMuted ? 0f : sfxVolume;

        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().volume = isMusicMuted ? 0f : musicVolume;

        isFullscreen = PlayerPrefs.GetInt("isFullscreen", 1) == 1 ? true : false;
        Screen.fullScreen = isFullscreen;
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt("isFullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetInt("isSfxMuted", isSfxMuted ? 1 : 0);
        PlayerPrefs.SetInt("isMusicMuted", isMusicMuted ? 1 : 0);
        PlayerPrefs.Save();
    }
    public void ToggleFullscreen()
    {
        isFullscreen = fullscreenToggle.isOn;
        Screen.fullScreen = isFullscreen;
        SaveSettings();
    }

    public void exitGame()
    {
        Application.Quit();
    }

    void OnApplicationQuit()
    {
        SaveSettings();
    }


    public void SetSfxVolume()
    {
        sfxVolume = sfxSlider.value;
        isSfxMuted = sfxMuteToggle.isOn;
        ApplySettings();
        SaveSettings();
    }

    public void SetMusicVolume()
    {
        musicVolume = musicSlider.value;
        isMusicMuted = musicMuteToggle.isOn;
        ApplySettings();
        SaveSettings();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (SceneManager.GetActiveScene().name != "Startscreen")
        {
            bool is2xSpeed = GameObject.Find("Speed").GetComponent<speedScript>().isTwoXSpeed;
        }
        
        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseImage.GetComponent<Image>().sprite = pausedSprite;
            pauseOverlay.SetActive(true);
        }
        else
        {
            if (SceneManager.GetActiveScene().name != "Startscreen")
            {
                bool is2xSpeed = GameObject.Find("Speed").GetComponent<speedScript>().isTwoXSpeed;

                if (is2xSpeed)
                {
                    Time.timeScale = 2f;
                }
                else
                {
                    Time.timeScale = 1f;
                }
            }
            pauseImage.GetComponent<Image>().sprite = normalSprite;
            pauseOverlay.SetActive(false);
        }
    }

    public void ReturnToMenu()
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

        gameOver = true;
        GameObject gameOverText = GameObject.FindGameObjectWithTag("GameOver");
        gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
        TogglePause();

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

    public void Restart()
    {
        int difficulty = PlayerPrefs.GetInt("difficulty");
        int gameScene = PlayerPrefs.GetInt("GameScene");
        int gameScene1 = PlayerPrefs.GetInt("GameScene1");
        int gameScene2 = PlayerPrefs.GetInt("GameScene2");
        int currentPanel = PlayerPrefs.GetInt("currentPanel");
        int wave = PlayerPrefs.GetInt("wave");
        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt("isFirstLoad", 0);

        SaveSettings();
        ApplySettings();

        PlayerPrefs.SetInt("difficulty", difficulty);
        PlayerPrefs.SetInt("GameScene", gameScene);
        PlayerPrefs.SetInt("GameScene1", gameScene1);
        PlayerPrefs.SetInt("GameScene2", gameScene2);
        PlayerPrefs.SetInt("currentPanel", currentPanel);
        PlayerPrefs.SetInt("wave", wave);

        string sceneName = SceneManager.GetActiveScene().name;
        
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

        TogglePause();
    }
}
