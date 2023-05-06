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

    public Toggle fullscreenToggle;
    public Slider sfxSlider;
    public Slider musicSlider;
    public Toggle sfxMuteToggle;
    public Toggle musicMuteToggle;
    public GameObject trackNameObj;

    // Settings variables
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

        if (SceneManager.GetActiveScene().name == "GameScene")
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
            if (SceneManager.GetActiveScene().name == "GameScene")
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
        SceneManager.LoadScene("Startscreen");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Gamescene");
        TogglePause();
    }
}
