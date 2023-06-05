using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScene : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        SliderHolder.slider = slider;
    }

    void Awake()
    {
        CheckFullscreenPreference();
    }


    public void CheckFullscreenPreference()
    {
        bool isFullscreen = PlayerPrefs.GetInt("isFullscreen", 1) == 1 ? true : false;
        Screen.fullScreen = isFullscreen;
    }

}

