using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startscreen : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        Screen.fullScreen = false;
    }




    void Update()
    {
        if (Input.anyKeyDown)
        {
            audioSource.Play();
            SceneManager.LoadScene("GameScene");
        }
    }
}
