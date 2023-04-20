using System.Collections;
using System.Collections.Generic;
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
    public GameObject volumeSlider;

    void Start()
    {
        pauseImage = transform.Find("pauseImage").gameObject;
        pauseOverlay.SetActive(false);
        volumeSlider.GetComponent<Slider>().value = AudioListener.volume;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseImage.GetComponent<Image>().sprite = pausedSprite;
            pauseOverlay.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseImage.GetComponent<Image>().sprite = normalSprite;
            pauseOverlay.SetActive(false);
        }
    }

    public void SetVolume()
    {
        AudioListener.volume = volumeSlider.GetComponent<Slider>().value;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Startscreen");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Gamescene");
    }
}
