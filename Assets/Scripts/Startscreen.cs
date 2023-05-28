using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startscreen : MonoBehaviour
{
    public AudioSource audioSource;
    private const float ASPECT_RATIO = 16f / 9f;
    private Camera mainCamera;

    void Start()
    {
        audioSource.Play();
        mainCamera = Camera.main;
        SetScaleableWindow();
    }

    void Update()
    {
    }

    void SetScaleableWindow()
    {

        mainCamera.aspect = ASPECT_RATIO;
        float scaleHeight = Screen.width / (16f * ASPECT_RATIO);
        float scaleWidth = Screen.height / 9f;
        float scale = Mathf.Min(scaleHeight, scaleWidth);
        mainCamera.orthographicSize = scale;
    }

}
