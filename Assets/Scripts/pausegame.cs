using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pausegame : MonoBehaviour
{
    public bool isPaused = false;
    private GameObject pauseImage;
    public Sprite pausedSprite;
    public Sprite normalSprite;
    public GameObject pauseOverlay;

    void Start()
    {
        pauseImage = transform.Find("pauseImage").gameObject;
        pauseOverlay.SetActive(false);
    }

    void Update()
    {
        if (isPaused && Input.anyKeyDown)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pause the game
            pauseImage.GetComponent<Image>().sprite = pausedSprite;
            pauseOverlay.SetActive(true); // Show the pause overlay
        }
        else
        {
            Time.timeScale = 1f; // Unpause the game
            pauseImage.GetComponent<Image>().sprite = normalSprite;
            pauseOverlay.SetActive(false); // Hide the pause overlay
        }
    }
}
